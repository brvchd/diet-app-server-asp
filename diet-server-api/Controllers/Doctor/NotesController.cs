using System.Threading.Tasks;
using diet_server_api.DTO.Requests.Doctor;
using diet_server_api.Exceptions;
using diet_server_api.Helpers;
using diet_server_api.Services.Interfaces.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace diet_server_api.Controllers.Doctor
{
    [ApiController]
    [Route("api/doctor/notes")]
    public class NotesController : ControllerBase
    {
        private readonly INotesRepository _notesRepo;


        public NotesController(INotesRepository notesRepo)
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
            catch (UserNotFound ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = "DOCTOR")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetNotes([FromQuery] int idPatient)
        {
            try
            {
                var response = await _notesRepo.GetNotes(idPatient);
                return Ok(response);
            }
            catch (UserNotFound ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}