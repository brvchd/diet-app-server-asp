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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateDiet(CreateDietRequest request)
        {
            try
            {
                var response = await _dietRepo.CreateDiet(request);
                return CreatedAtAction(nameof(CreateDiet), response);
            }
            catch (AlreadyExists ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "DOCTOR")]
        [Route("mealtake")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AssignMealTake(AssignMealsRequest request)
        {
            try
            {
                await _dietRepo.AssignMeal(request);
                return Ok();
            }
            catch (AlreadyExists ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpGet]
        [Authorize(Roles = "DOCTOR")]
        [Route("days")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDays([FromBody] int idDiet)
        {
            try
            {
                var response = await _dietRepo.GetDays(idDiet);
                return Ok(response);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
        }



    }
}