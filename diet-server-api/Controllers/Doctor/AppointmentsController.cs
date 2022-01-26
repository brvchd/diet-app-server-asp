using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using diet_server_api.Exceptions;
using diet_server_api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace diet_server_api.Controllers.Doctor
{
    [ApiController]
    [Route("api/doctors/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentsService _appointmentService;

        public AppointmentsController(IAppointmentsService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet]
        [Authorize(Roles = "DOCTOR")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAppointments()
        {
            var user = HttpContext.User;
            var nameIdentifier = int.Parse(user.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            try
            {
                var results = await _appointmentService.GetDoctorAppointmentDates(nameIdentifier);
                return Ok(results);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet]
        [Route("dates")]
        [Authorize(Roles = "DOCTOR")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAppointmentsByDate(DateTime request)
        {
            var user = HttpContext.User;
            var nameIdentifier = int.Parse(user.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            try
            {
                var results = await _appointmentService.GetDoctorAppointmentsByDates(request, nameIdentifier);
                return Ok(results);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }

        }
        [HttpGet]
        [Route("details/{idVisit}")]
        [Authorize(Roles = "DOCTOR")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAppointmentsDetails([FromRoute] int idVisit)
        {
            try
            {
                var results = await _appointmentService.GetDoctorAppointmentDetails(idVisit);
                return Ok(results);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }

        }
    }
}