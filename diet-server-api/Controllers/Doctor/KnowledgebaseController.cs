using System.Threading.Tasks;
using diet_server_api.DTO.Requests.KnowledgeBase.Add;
using diet_server_api.Exceptions;
using diet_server_api.Models;
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AddDisease(AddDiseaseRequest request)
        {
            try
            {
                var response = await _knowledgeRepo.AddDisease(request);
                return CreatedAtAction(nameof(AddDisease), response);
            }
            catch (AlreadyExists ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost]
        [Route("supplement")]
        [Authorize(Roles = "DOCTOR")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AddSupplement(AddSupplementRequest request)
        {
            try
            {
                var response = await _knowledgeRepo.AddSupplement(request);
                return CreatedAtAction(nameof(AddSupplement), response);
            }
            catch (AlreadyExists ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("supplements")]
        [Authorize(Roles = "DOCTOR")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetSupplements(int page)
        {
            var response = await _knowledgeRepo.GetSupplements(page);
            return Ok(response);
        }
        [HttpGet]
        [Route("diseases")]
        [Authorize(Roles = "DOCTOR")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetDiseases(int page)
        {
            var response = await _knowledgeRepo.GetDiseases(page);
            return Ok(response);
        }

        [HttpGet]
        [Route("diseases/search")]
        [Authorize(Roles = "DOCTOR")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SearchDisease([FromQuery] string diseaseName)
        {
            try
            {
                var response = await _knowledgeRepo.SearchDisease(diseaseName);
                return Ok(response);
            }
            catch (InvalidData ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("supplement/search")]
        [Authorize(Roles = "DOCTOR")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> SearchSupplement([FromQuery] string supplementName)
        {
            try
            {
                var response = await _knowledgeRepo.SearchSupplement(supplementName);
                return Ok(response);
            }
            catch (InvalidData ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpPost]
        [Route("product")]
        [Authorize(Roles = "DOCTOR")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddProduct(AddProductRequest request)
        {
            try
            {
                var response = await _knowledgeRepo.AddProduct(request);
                return Ok(response);
            }
            catch (AlreadyExists ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("products")]
        [Authorize(Roles = "DOCTOR")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var response = await _knowledgeRepo.GetProducts();
                return Ok(response);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("parameters")]
        [Authorize(Roles = "DOCTOR")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetParameters()
        {
            try
            {
                var response = await _knowledgeRepo.GetParameters();
                return Ok(response);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Route("parameter")]
        [Authorize(Roles = "DOCTOR")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AddParameter(AddParameterRequest request)
        {
            try
            {
                var response = await _knowledgeRepo.AddParameter(request);
                return Ok(response);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}