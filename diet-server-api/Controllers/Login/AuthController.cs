using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using diet_server_api.DTO.Requests.Auth;
using diet_server_api.Exceptions;
using diet_server_api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace diet_server_api.Controllers.Login
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService = null)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            try
            {
                var response = await _authService.Login(request);
                return Ok(response);

            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidData ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UserIsPending ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("logout")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Logout(AccountManage request)
        {
            var user = HttpContext.User;
            var nameIdentifier = int.Parse(user.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            if (nameIdentifier != request.IdUser)
            {
                return Forbid();
            }
            try
            {
                await _authService.Logout(request.IdUser);
                return Ok();

            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("refresh")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequest request)
        {
            try
            {
                var response = await _authService.RefreshToken(request);
                return Ok(response);
            }
            catch (RefreshTokenExpired ex)
            {
                return BadRequest(ex.Message);
            }
            catch (RefreshTokenNotFound ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}