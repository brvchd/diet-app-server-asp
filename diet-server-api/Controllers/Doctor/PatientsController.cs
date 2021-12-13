using System;
using System.Threading.Tasks;
using diet_server_api.DTO.Requests.Patient;
using diet_server_api.DTO.Responses.Doctor.Get;
using diet_server_api.DTO.Responses.Doctor.Search;
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
        private readonly IPatientRepository _patientRepo;
        private readonly IMeasurementRepository _measurementRepo;

        public PatientsContoller(IPatientRepository patientRepo, IMeasurementRepository measurementRepo)
        {
            _patientRepo = patientRepo;
            _measurementRepo = measurementRepo;
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
        [Route("patient/info")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetPatientData(int idpatient)
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddMeasurement(AddMeasrumentsRequest request)
        {
            try
            {
                var response = await _measurementRepo.AddMeasruments(request, Roles.DOCTOR);
                return CreatedAtAction(nameof(AddMeasurement), response);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet]
        [Route("patient/measurements")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMeasurements(int idPatient)
        {
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
        [Route("patient/measurementsbydate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMeasurementsByDate(int idPatient, DateTime requestedDate)
        {
            try
            {
                var response = await _measurementRepo.GetMeasurements(idPatient, requestedDate);
                return Ok(response);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
            catch(InvalidData ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}