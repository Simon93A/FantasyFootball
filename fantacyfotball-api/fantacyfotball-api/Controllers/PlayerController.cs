using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using fantacyfotball_api.Models;
using Microsoft.EntityFrameworkCore;
using fantacyfotball_api.Models.DTOs;

namespace fantacyfotball_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly FantasyFootballDbContext _db;
        public PlayerController(FantasyFootballDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPlayers()
        {
            var players = await _db.Players.AsNoTracking().ToListAsync();

            return Ok(players);
        }
        [HttpPost("{playerId}")]
        public async Task<IActionResult> AddPlayerToTeam(AddPlayerToTeamDTO dto)
        {
            var player = await _db.Players.SingleOrDefaultAsync(p => p._id == dto.playerId);
            var user = await _db.Users.SingleOrDefaultAsync(u => u._id == dto.userId);
            var team = await _db.Teams.SingleOrDefaultAsync(t => t._id == t._id);
            

            if (player == null)
            {
                return NotFound($"Spelaren kunde inte hittas.");
            }
            if (user == null)
            {
                return NotFound($"Användaren kunde inte hittas.");
            }
            if (user.TeamId == null)
            {
                return NotFound($"Användaren har inget lag.");
            }

            team.PlayersID.Add(player._id);
            await _db.SaveChangesAsync();

            return Ok($"{player.Name} är nu med i ditt lag. Grattis!");
        }
        [HttpDelete]
        public async Task<IActionResult> DeletePlayerFromDb(int playerId)
        {
            var player = _db.Players.SingleOrDefault(p => p._id == playerId);

            if (player == null )
            {
                return NotFound("Could not find the player.");
            }

            _db.Players.Remove(player);
            await _db.SaveChangesAsync();

            return Ok($"{player.Name} has been removed.");
        }
       
    }
}
