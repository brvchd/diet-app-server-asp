using System.Threading.Tasks;
using diet_server_api.DTO.Requests.Doctor;
using diet_server_api.Exceptions;
using diet_server_api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace diet_server_api.Controllers.doctor
{
    [ApiController]
    [Route("api/doctor/pending")]
    public class PendingController : ControllerBase
    {
        private readonly IDoctorPendingService _doctorService;

        public PendingController(IDoctorPendingService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        [Authorize(Roles = "DOCTOR")]
        [Route("patients")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetPatients()
        {
            var response = await _doctorService.GetPendingPatients();
            return Ok(response);
        }

        [HttpGet]
        [Authorize(Roles = "DOCTOR")]
        [Route("patient")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetPatient(int idpatient)
        {
            try
            {
                var response = await _doctorService.GetPendingPatientData(idpatient);
                return Ok(response);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPatch]
        [Authorize(Roles = "DOCTOR")]
        [Route("patient")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AcceptPatient(PendingPatientAccept request)
        {
            try
            {
                await _doctorService.AcceptPendingPatient(request);
                return Ok();
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete]
        [Authorize(Roles = "DOCTOR")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RejectPatient(int idPatient)
        {
            try
            {
                await _doctorService.RejectPendingPatient(idPatient);
                return Ok("Patient rejected successfully");
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}