using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using diet_server_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace diet_server_api.Controllers.dev
{
    [ApiController]
    [Route("api/[controller]")]
    public class DevController : ControllerBase
    {
        private readonly mdzcojxmContext _dbContext;

        public DevController(mdzcojxmContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        [Route("tempuser")]
        public async Task<IActionResult> AddTempUser(TemporaryUser user)
        {
            var exists = await _dbContext.TempUsers.AnyAsync(e => e.Email == user.Email);
            if(exists) return BadRequest();
            var tempUser = new TempUser(){
                Email = user.Email,
                Uniquekey = user.UniqueKey
            };
            await _dbContext.TempUsers.AddAsync(tempUser);
            await _dbContext.SaveChangesAsync();
            return Ok(tempUser);
        }

        [HttpGet]
        [Route("tempusers")]
        public async Task<IActionResult> GetTempUser()
        {
            return Ok(await _dbContext.TempUsers.ToListAsync());
        }
        [HttpGet]
        [Route("users")]
        public async Task<IActionResult> GetUsers ()
        {
            return Ok(await _dbContext.Users.ToListAsync());
        }

    }
    public class TemporaryUser
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string UniqueKey { get; set; }
    }
}