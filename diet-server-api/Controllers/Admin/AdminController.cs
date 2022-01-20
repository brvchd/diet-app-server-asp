using System.Threading.Tasks;
using diet_server_api.DTO.Requests.Admin;
using diet_server_api.DTO.Requests.Auth;
using diet_server_api.Exceptions;
using diet_server_api.Services.Interfaces.Repository;
using Microsoft.AspNetCore.Authorization;
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
        [Route("dev/tempuser")]
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
        [Route("dev/tempusers")]
        public async Task<IActionResult> GetTempUsers()
        {
            var response = await _tempUserService.GetTempUsers();
            return Ok(response);
        }
        [HttpGet]
        [Route("dev/users")]
        public async Task<IActionResult> GetALLUsers()
        {

            var response = await _userService.GetUsers();
            return Ok(response);
        }

        [HttpPost]
        [Route("users")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
            catch (InvalidData ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("users")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetUsers(int Page)
        {
            try
            {
                var results = await _adminService.GetUsers(Page);
                return Ok(results);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut]
        [Route("users/accounts/activate")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ActivateAccount(AccountManage requqest)
        {
            try
            {
                await _adminService.ActiveAccount(requqest.IdUser);
                return Ok();
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPut]
        [Route("users/accounts/deactivate")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeactivateAccount(AccountManage request)
        {
            try
            {
                await _adminService.DeactiveAccount(request.IdUser);
                return Ok();
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet]
        [Route("users/search")]
        [Authorize(Roles = "ADMIN")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> SearchUser([FromQuery] string firstName, [FromQuery] string lastName)
        {
            try
            {
                var response = await _adminService.SearchUser(firstName, lastName);
                return Ok(response);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
        }
    }

}