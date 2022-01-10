using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using diet_server_api.DTO.Requests.Patient;
using diet_server_api.Exceptions;
using diet_server_api.Helpers;
using diet_server_api.Services.Interfaces.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace diet_server_api.Controllers.Doctor
{
    [ApiController]
    [Route("api/doctor")]
    public class PatientsContoller : ControllerBase
    {
        private readonly IPatientService _patientRepo;
        private readonly IMeasurementService _measurementRepo;
        private readonly IDiseaseService _diseaseRepo;

        public PatientsContoller(IPatientService patientRepo, IMeasurementService measurementRepo, IDiseaseService diseaseRepo)
        {
            _patientRepo = patientRepo;
            _measurementRepo = measurementRepo;
            _diseaseRepo = diseaseRepo;
        }

        [HttpGet]
        [Authorize(Roles = "DOCTOR")]
        [Route("patients")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetPatients(int page)
        {
            var response = await _patientRepo.GetPatientPage(page);
            return Ok(response);
        }

        [HttpGet]
        [Route("patients/search")]
        [Authorize(Roles = "DOCTOR")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SearchPatients([FromQuery] string firstName, [FromQuery] string lastName)
        {

            try
            {
                var response = await _patientRepo.GetPatientsByName(firstName, lastName);
                return Ok(response);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = "DOCTOR")]
        [Route("patient/info/{idPatient}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetPatientData([FromRoute] int idpatient)
        {
            try
            {
                var response = await _patientRepo.GetPatientInfo(idpatient);
                return Ok(response);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Route("patient/measurements")]
        [Authorize(Roles = "DOCTOR, PATIENT")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddMeasurement(AddMeasrumentsRequest request)
        {
            var user = HttpContext.User;
            var nameIdentifier = int.Parse(user.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            if (user.IsInRole("PATIENT") && nameIdentifier != request.Idpatient)
            {
                return Forbid();
            }

            try
            {
                var role = user.IsInRole("DOCTOR") ? Roles.DOCTOR : Roles.PATIENT;
                var response = await _measurementRepo.AddMeasurements(request, role);
                return CreatedAtAction(nameof(AddMeasurement), response);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
            catch (AlreadyExists ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("patient/measurements/{idPatient}")]
        [Authorize(Roles = "DOCTOR, PATIENT")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMeasurements([FromRoute] int idPatient)
        {
            var user = HttpContext.User;
            var nameIdentifier = int.Parse(user.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            if (user.IsInRole("PATIENT") && nameIdentifier != idPatient)
            {
                return Forbid();
            }

            try
            {
                var response = await _measurementRepo.GetMeasurements(idPatient);
                return Ok(response);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet]
        [Authorize(Roles = "DOCTOR, PATIENT")]
        [Route("patient/measurementsbydate/{idPatient}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMeasurementsByDate([FromRoute] int idPatient, [FromQuery] DateTime requestedDate, [FromQuery] string whomeasured)
        {
            var user = HttpContext.User;
            var nameIdentifier = int.Parse(user.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            if (user.IsInRole("PATIENT") && nameIdentifier != idPatient)
            {
                return Forbid();
            }
            try
            {
                var response = await _measurementRepo.GetMeasurements(idPatient, requestedDate, whomeasured);
                return Ok(response);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidData ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("patient/measurement/{idPatient}")]
        [Authorize(Roles = "DOCTOR, PATIENT")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMeasurement([FromRoute] int idPatient)
        {
            var user = HttpContext.User;
            var nameIdentifier = int.Parse(user.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            if (user.IsInRole("PATIENT") && nameIdentifier != idPatient)
            {
                return Forbid();
            }
            try
            {
                var response = await _measurementRepo.GetMeasurement(idPatient);
                return Ok(response);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("patient/diseases/{idPatient}")]
        [Authorize(Roles = "DOCTOR")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPatientDiseases([FromRoute] int idPatient)
        {
            try
            {
                var response = await _diseaseRepo.GetPatientDiseases(idPatient);
                return Ok(response);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}