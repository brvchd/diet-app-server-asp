using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using diet_server_api.DTO.Requests.Patient;
using diet_server_api.Exceptions;
using diet_server_api.Services.Interfaces.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace diet_server_api.Controllers.Patient
{
    [ApiController]
    [Route("api/patient/[controller]")]
    public class DietsController : ControllerBase
    {
        private readonly IPatientService _patService;

        public DietsController(IPatientService patService)
        {
            _patService = patService;
        }

        [HttpPut]
        [Route("days")]
        [Authorize(Roles = "PATIENT")]
        public async Task<IActionResult> FillDietDay(FillDayReportRequest request)
        {
            var user = HttpContext.User;
            var nameIdentifier = int.Parse(user.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            try
            {
                await _patService.FillReport(request, nameIdentifier);
                return Ok();
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
            catch(AlreadyExists ex)
            {
                return BadRequest(ex.Message);
            }
            catch(InvalidData ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}