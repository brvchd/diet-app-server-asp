using System.Threading.Tasks;
using diet_server_api.DTO.Requests;
using diet_server_api.Exceptions;
using diet_server_api.Services.Interfaces;
using diet_server_api.Services.Interfaces.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace diet_server_api.Controllers
{
    [ApiController]
    [Route("api/survey")]
    public class SurveyController : ControllerBase
    {
        private readonly ISurveyService _surveyService;
        private readonly IPatientRepository _patientRepoService;

        public SurveyController(ISurveyService surveyService, IPatientRepository patientRepoService)
        {
            _surveyService = surveyService;
            _patientRepoService = patientRepoService;
        }

        [HttpPost("access")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Access(SurveyCheckCredentialsRequest request)
        {
            try
            {
                var response = await _surveyService.ValidateSurveyCredentials(request);
                return Ok(response);
            }
            catch (UserNotFound ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost("signup")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SignUp(SurveySignUpRequest request)
        {
            try
            {
                var response = await _patientRepoService.CreatePatient(request);
                return CreatedAtAction(nameof(SignUp), response);
            }
            catch (UserAlreadyExists ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UserNotFound ex)
            {
                return NotFound(ex.Message);
            }

        }
    }
}