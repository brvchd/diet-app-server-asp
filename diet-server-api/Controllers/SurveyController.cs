using System.Threading.Tasks;
using diet_server_api.DTO.Requests;
using diet_server_api.Exceptions;
using diet_server_api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace diet_server_api.Controllers
{
    [ApiController]
    [Route("survey")]
    public class SurveyController : ControllerBase
    {
        private readonly ISurvey _surveyService;
        public SurveyController(ISurvey surveyService)
        {
            _surveyService = surveyService;
        }

        [HttpPost("access")]
        public async Task<IActionResult> Access(SurveyCheckCredentialsRequest request)
        {
            try
            {
                var userExists = await _surveyService.ValidateSurveyCredentialsAsync(request);
                return Ok();
            }
            catch (UserDoesNotExistsException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(SurveySignUpRequest request)
        {

            return Ok();
        }
        [HttpPost("test")]
        public IActionResult test(testbody body)
        {
            return Ok(body);
        }
    }
}