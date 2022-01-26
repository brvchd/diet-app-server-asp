using System.Threading.Tasks;
using diet_server_api.Exceptions;
using diet_server_api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace diet_server_api.Controllers.Doctor
{
    [ApiController]
    [Route("api/[controller]")]
    public class InformationsController : ControllerBase
    {
        private readonly IInformationService _infService;
        public InformationsController(IInformationService infService)
        {
            _infService = infService;
        }

        [HttpGet]
        [Route("full/{idPatient}")]
        public async Task<IActionResult> GetFullInfo([FromRoute] int idPatient, [FromQuery] int? idDiet)
        {
            try
            {
                var response = await _infService.GetFullPatientInfo(idPatient, idDiet);
                return Ok(response);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet]
        [Route("mid/{idPatient}")]
        public async Task<IActionResult> GetMidInfo([FromRoute] int idPatient)
        {
            try
            {
                var response = await _infService.GetMidPatientInfo(idPatient);
                return Ok(response);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet]
        [Route("less/{idPatient}")]
        public async Task<IActionResult> GetLessInfo([FromRoute] int idPatient, [FromQuery] int? idDiet)
        {
            try
            {
                var response = await _infService.GetLessInfoResponse(idPatient, idDiet);
                return Ok(response);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
        } 
    }
}