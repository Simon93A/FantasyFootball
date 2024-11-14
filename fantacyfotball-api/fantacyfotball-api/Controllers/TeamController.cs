using fantacyfotball_api.Models;
using fantacyfotball_api.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace fantacyfotball_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly FantasyFootballDbContext _context;
        private readonly ILogger<TeamController> _logger;

        public TeamController(FantasyFootballDbContext context, ILogger<TeamController> logger)
            
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("{userName}")]
        public async Task<IActionResult> GetTeamByUserName(string userName)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == userName);
            if (user == null) return BadRequest();

            var team = await _context.Teams.FirstOrDefaultAsync(t => t.UserID == user._id);
            if (team == null) return NotFound("no team found");

            var players = await _context.Players.Where(p=>team.PlayersID.Contains(p._id)).ToListAsync();
            var teamDTo = new ShowTeamDTO 
            { 
                _id =team._id,
                TeamName = team.Name,
                Players = players,
                UserID = user._id,
            };

            return Ok(teamDTo);
        }
        [HttpPost("InsertTeam")]
        public async Task<IActionResult> CreateTeam(CreateTeamDTO team)
        {

            var user = await _context.Users.SingleOrDefaultAsync(u => u._id == team.userID);
            if (user == null) return BadRequest();

            bool budget = await ValidateTeamBudget(team, user);
            if (!budget) 
            {
                return BadRequest("Too many players or insufficient funds.");
            }
           
            if(!string.IsNullOrEmpty(user.TeamId))
            {
               var selectedTeam = await _context.Teams.SingleOrDefaultAsync(t => t._id == user.TeamId);

                var newTeam2 = new Team
                {
                    _id = selectedTeam!._id,
                    Name = team.TeamName,
                    UserID = team.userID,
                    PlayersID = team.PlayerIDList,
                };
                _context.Entry(selectedTeam!).CurrentValues.SetValues(newTeam2);
                await _context.SaveChangesAsync();

            }
            else
            {
                var newTeam = new Team
                {
                    _id = Guid.NewGuid().ToString(),
                    Name = team.TeamName,
                    UserID = team.userID,
                    PlayersID = team.PlayerIDList,
                };
                var newUserVal = new User()
                {
                    _id = user._id,
                    Money = user.Money,
                    UserName = user.UserName,
                    TeamId = newTeam._id

                };
                await _context.Teams.AddAsync(newTeam);
                await _context.SaveChangesAsync();

                _context.Entry(user).CurrentValues.SetValues(newUserVal);
                await _context.SaveChangesAsync();
            }
           

            return Ok("Team Created");
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllTeams()
        {
            var teams = await _context.Teams.ToListAsync();

            return Ok(teams);
        }

        [HttpGet("GetTeamById")]
        public async Task<IActionResult> GetTeamById(string id)
        {
            var teams = await _context.Teams.FirstOrDefaultAsync(t=>t._id == id);   

            return Ok(teams);
        }

        private async Task<bool> ValidateTeamBudget(CreateTeamDTO dto, User user)
        {

            var playerList = await _context.Players
                                            .Where(p => dto.PlayerIDList.Contains(p._id))
                                            .ToListAsync();

            decimal totalPrice = playerList.Sum(player => player.Price);


            if (dto.PlayerIDList.Count > 11 || user.Money < totalPrice)
            {
                return false;
            }
            return true;
        }

    }
}
