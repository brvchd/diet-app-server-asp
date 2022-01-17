using System.Linq;
using System.Threading.Tasks;
using diet_server_api.DTO.Requests.KnowledgeBase.Add;
using diet_server_api.Exceptions;
using diet_server_api.Services.Interfaces.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;

namespace diet_server_api.Controllers.Doctor
{
    [ApiController]
    [Route("api/notes")]
    public class NotesController : ControllerBase
    {
        private readonly INotesService _notesRepo;


        public NotesController(INotesService notesRepo)
        {
            _notesRepo = notesRepo;
        }


        [HttpPost]
        [Authorize(Roles = "DOCTOR")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AddNote(AddNoteRequest request)
        {
            try
            {
                var response = await _notesRepo.AddNote(request);
                return CreatedAtAction(nameof(AddNote), response);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{idPatient}")]
        [Authorize(Roles = "DOCTOR, PATIENT")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetNotes([FromRoute] int idPatient)
        {
            var user = HttpContext.User;
            var nameIdentifier = int.Parse(user.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            if (user.IsInRole("PATIENT") && nameIdentifier != idPatient)
            {
                return Forbid();
            }

            try
            {
                var response = await _notesRepo.GetNotes(idPatient);
                return Ok(response);
            }
            catch (NotFound ex)
            {
                return NotFound(ex.Message);
            }
            catch (NotActive ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}