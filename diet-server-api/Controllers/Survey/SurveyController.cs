using System.Threading.Tasks;
using diet_server_api.DTO.Requests;
using diet_server_api.Exceptions;
using diet_server_api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace diet_server_api.Controllers
{
    [ApiController]
    [Route("api/survey")]
    public class SurveyController : ControllerBase
    {
        private readonly ISurveyService _surveyService;
        public SurveyController(ISurveyService surveyService)
        {
            _surveyService = surveyService;
        }

        [HttpPost("access")]
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
        public async Task<IActionResult> SignUp(SurveySignUpRequest request)
        {
            try
            {
                var response = await _surveyService.CreateUserFromSurvey(request);
                return CreatedAtAction("signup", response);
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