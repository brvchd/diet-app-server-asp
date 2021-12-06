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
        private readonly IDoctorRepository _doctorRepoService;
        private readonly ITempUserRepository _tempUserRepoService;
        private readonly IUserRepository _userRepo;

        public AdminController(IDoctorRepository doctorRepoService, ITempUserRepository tempUserRepoService, IUserRepository userRepo)
        {
            _doctorRepoService = doctorRepoService;
            _tempUserRepoService = tempUserRepoService;
            _userRepo = userRepo;
        }

        [HttpPost]
        [Route("tempuser")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddTempUser(TemporaryUserRequest user)
        {
            try
            {
                var response = await _tempUserRepoService.AddTempUser(user);
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
            var response = await _tempUserRepoService.GetTempUsers();
            return Ok(response);
        }
        [HttpGet]
        [Route("users")]
        public async Task<IActionResult> GetUsers()
        {

            var response = await _userRepo.GetUsers();
            return Ok(response);
        }

        [HttpPost]
        [Route("doctor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateDoctor(DoctorCreatorRequest request)
        {
            try
            {
                var response = await _doctorRepoService.CreateDoctor(request);
                return CreatedAtAction(nameof(CreateDoctor), response);
            }
            catch (AlreadyExists ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }

}