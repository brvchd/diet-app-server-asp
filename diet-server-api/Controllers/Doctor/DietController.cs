using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using diet_server_api.DTO.Requests.Diet;
using diet_server_api.Exceptions;
using diet_server_api.Models;
using diet_server_api.Services.Interfaces.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace diet_server_api.Controllers.Doctor
{
    [ApiController]
    [Route("api/doctor/diet")]
    public class DietController : ControllerBase
    {
        private readonly IDietService _dietRepo;
        private readonly mdzcojxmContext _dbContext;

        public DietController(IDietService dietRepo, mdzcojxmContext dbContext)
        {
            _dietRepo = dietRepo;
            _dbContext = dbContext;
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
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
            catch (NotActive ex)
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
        [Route("days/{idDiet}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDays([FromRoute] int idDiet)
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

        [HttpGet]
        [Authorize(Roles = "DOCTOR")]
        [Route("diets/{idPatient}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPatietDiets([FromRoute] int idPatient)
        {
            try
            {
                var response = await _dietRepo.GetPatientDiets(idPatient);
                return Ok(response);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
            catch (NotActive ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Authorize(Roles = "DOCTOR, PATIENT")]
        [Route("meals/{idDiet}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDietMeals([FromRoute] int idDiet)
        {
            var user = HttpContext.User;
            var nameIdentifier = int.Parse(user.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var isPatientDiet = await _dbContext.Diets.AnyAsync(e => e.Iddiet == idDiet && e.Idpatient == nameIdentifier);
            if (user.IsInRole("PATIENT") && !isPatientDiet)
            {
                return Forbid();
            }
            try
            {
                var response = await _dietRepo.GetDietMeals(idDiet);
                return Ok(response);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}