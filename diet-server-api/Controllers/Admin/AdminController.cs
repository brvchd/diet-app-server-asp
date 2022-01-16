using System.Threading.Tasks;
using diet_server_api.DTO.Requests.Admin;
using diet_server_api.Exceptions;
using diet_server_api.Services.Interfaces.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace diet_server_api.Controllers.Admin
{
    [ApiController]
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly ITempUserService _tempUserService;
        private readonly IUserService _userService;

        public AdminController(IAdminService doctorRepoService, ITempUserService tempUserRepoService, IUserService userRepo)
        {
            _adminService = doctorRepoService;
            _tempUserService = tempUserRepoService;
            _userService = userRepo;
        }

        [HttpPost]
        [Route("tempuser")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddTempUser(TemporaryUserRequest user)
        {
            try
            {
                var response = await _tempUserService.AddTempUser(user);
                return CreatedAtAction(nameof(AddTempUser), response);

            }
            catch (AlreadyExists ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("tempusers")]
        public async Task<IActionResult> GetTempUsers()
        {
            var response = await _tempUserService.GetTempUsers();
            return Ok(response);
        }
        [HttpGet]
        [Route("users")]
        public async Task<IActionResult> GetUsers()
        {

            var response = await _userService.GetUsers();
            return Ok(response);
        }

        [HttpPost]
        [Route("users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser(CreateUserRequest request)
        {
            try
            {
                var response = await _adminService.CreateUser(request);
                return CreatedAtAction(nameof(CreateUser), response);
            }
            catch (AlreadyExists ex)
            {
                return BadRequest(ex.Message);
            }
            catch(InvalidData ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }

}