using System.Threading.Tasks;
using diet_server_api.DTO.Requests.Diet;
using diet_server_api.Exceptions;
using diet_server_api.Services.Interfaces.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace diet_server_api.Controllers.Doctor
{
    [ApiController]
    [Route("api/doctor/diet")]
    public class DietController : ControllerBase
    {
        private readonly IDietRepository _dietRepo;

        public DietController(IDietRepository dietRepo)
        {
            _dietRepo = dietRepo;
        }

        [HttpPost]
        [Authorize(Roles = "DOCTOR")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult>CreateDiet(CreateDietRequest request)
        {
            try
            {
                 var response = await _dietRepo.CreateDiet(request);
                 return Ok(response);
            }
            catch (AlreadyExists ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}