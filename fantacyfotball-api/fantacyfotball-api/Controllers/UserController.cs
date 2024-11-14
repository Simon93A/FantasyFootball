using fantacyfotball_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using fantacyfotball_api.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace fantacyfotball_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly FantasyFootballDbContext _context;

        public UserController(FantasyFootballDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDTO user)
        {
           var selectedUser = await _context.Users.SingleOrDefaultAsync(u=>u.UserName == user.UserName);
            if (selectedUser != null)
                return BadRequest("Username taken");

            var newUser = new User
            {
                _id = Guid.NewGuid().ToString(),
                UserName = user.UserName,
                Money = 1000,
            };

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return Ok("User Created");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = _context.Users.ToList();

            return Ok(users);
        }

        [HttpGet("UserName")]
        public async Task<IActionResult> GetUserByUserName(string userName)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserName == userName);

            //Som en vanlig if-sats men minimal
            return user is not null ? Ok(user) : NotFound("User not found");    
        }

   
    }
}
