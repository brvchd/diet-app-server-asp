using System;
using System.Threading.Tasks;
using diet_server_api.DTO.Requests.Secretary;
using diet_server_api.Exceptions;
using diet_server_api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace diet_server_api.Controllers.Secretary
{
    [ApiController]
    [Route("api/secretary")]
    public class SecretaryController : Controller
    {
        private readonly ISecretaryService _secService;
        public SecretaryController(ISecretaryService secService)
        {
            _secService = secService;
        }

        [HttpGet]
        [Route("patients/search")]
        [Authorize(Roles = "SECRETARY")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> SearchPatient([FromQuery] string firstname, [FromQuery] string lastname)
        {
            try
            {
                var results = await _secService.SearchPatient(firstname, lastname);
                return Ok(results);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("doctors/search")]
        [Authorize(Roles = "SECRETARY")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> SearchDoctor([FromQuery] string firstname, [FromQuery] string lastname)
        {
            try
            {
                var results = await _secService.SearchDoctor(firstname, lastname);
                return Ok(results);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("appointments")]
        [Authorize(Roles = "SECRETARY")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAppointments()
        {
            try
            {
                var results = await _secService.GetAppointmentDates();
                return Ok(results);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpDelete]
        [Route("appointments")]
        [Authorize(Roles = "SECRETARY")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CancelAppointment(int idVisit)
        {
            try
            {
                await _secService.CancelAppointment(idVisit);
                return Ok();
            }
            catch (InvalidData ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("appointments/dates")]
        [Authorize(Roles = "SECRETARY")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAppointmentsByDate(DateTime request)
        {
            try
            {
                var results = await _secService.GetAppointmentsByDates(request);
                return Ok(results);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }

        }
        [HttpGet]
        [Route("appointments/details/{idVisit}")]
        [Authorize(Roles = "SECRETARY")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAppointmentsDetails([FromRoute] int idVisit)
        {
            try
            {
                var results = await _secService.GetAppointmentDetails(idVisit);
                return Ok(results);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }

        }


        [HttpPost]
        [Authorize(Roles = "SECRETARY")]
        [Route("emails")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> SendEmail(SendEmailRequest request)
        {
            await _secService.SendEmail(request);
            return Ok();
        }
        [HttpPost]
        [Route("visits")]
        [Authorize(Roles = "SECRETARY")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> BookVisit(AssignAppointmentRequest request)
        {
            try
            {
                await _secService.AssignAppointment(request);
                return CreatedAtAction(nameof(BookVisit), request);
            }
            catch (NotActive ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
            catch (AlreadyExists ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidData ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}