using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public PatientsContoller(IPatientRepository patientRepo)
        {
            _patientRepo = patientRepo;
        }

        [HttpGet]
        [Authorize(Roles = "DOCTOR")]
        [Route("patients")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetPatients(int page)
        {
            var response = await _patientRepo.GetPatientsByPage(page);
            return Ok(response);
        }
    }
}