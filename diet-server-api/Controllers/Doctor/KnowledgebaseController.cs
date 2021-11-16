using System.Threading.Tasks;
using diet_server_api.DTO.Requests.Doctor;
using diet_server_api.Exceptions;
using diet_server_api.Helpers;
using diet_server_api.Services.Interfaces.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace diet_server_api.Controllers.Doctor
{
    [ApiController]
    [Route("api/knowledgebase")]
    public class KnowledgebaseController : ControllerBase
    {
        private readonly IKnowledgeBaseRepository _knowledgeRepo;

        public KnowledgebaseController(IKnowledgeBaseRepository knowledgeRepo)
        {
            _knowledgeRepo = knowledgeRepo;
        }


        [HttpPost]
        [Route("disease")]
        [Authorize(Roles = "DOCTOR")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddDisease(AddDiseaseRequest request)
        {
            try
            {
                var response = await _knowledgeRepo.AddDisease(request);
                return CreatedAtAction(nameof(AddDisease), response);
            }
            catch (DiseaseAlreadyExists ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost]
        [Route("supplement")]
        [Authorize(Roles = "DOCTOR")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddSupplement(AddSupplementRequest request)
        {
            try
            {
                var response = await _knowledgeRepo.AddSupplement(request);
                return CreatedAtAction(nameof(AddSupplement), response);
            }
            catch (SupplementAlreadyExists ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}