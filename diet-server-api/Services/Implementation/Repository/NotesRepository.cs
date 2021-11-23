using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using diet_server_api.DTO.Requests.Doctor;
using diet_server_api.DTO.Responses.Doctor;
using diet_server_api.Exceptions;
using diet_server_api.Models;
using diet_server_api.Services.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace diet_server_api.Services.Implementation.Repository
{
    public class NotesRepository : INotesRepository
    {
        private readonly mdzcojxmContext _dbContext;

        public NotesRepository(mdzcojxmContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AddNoteResponse> AddNote(AddNoteRequest request)
        {
            var patientExists = await _dbContext.Patients.AnyAsync(e => e.Iduser == request.IdPatient);
            if (!patientExists) throw new UserNotFound("Patient not found!");
            var doctorExists = await _dbContext.Doctors.AnyAsync(e => e.Iduser == request.IdDoctor);
            if (!doctorExists) throw new UserNotFound("Doctor not found!");

            var note = new Note()
            {
                Iddoctor = request.IdDoctor,
                Idpatient = request.IdPatient,
                Message = request.Note
            };

            await _dbContext.Notes.AddAsync(note);
            await _dbContext.SaveChangesAsync();

            return new AddNoteResponse()
            {
                IdNote = note.Idnote,
                Message = note.Message
            };
        }

        public async Task<List<GetNotesResponse>> GetNotes(int idPatient = 1)
        {
            var patientExists = await _dbContext.Patients.AnyAsync(e => e.Iduser == idPatient && e.Ispending == false);
            if (!patientExists) throw new UserNotFound("Patient not found!");
            var notes = await _dbContext.Notes.Where(e => e.Idpatient == idPatient).Select(e => new GetNotesResponse
            {
                NoteText = e.Message,
                NoteCreated = e.Dateofnote
            }).OrderByDescending(e => e.NoteCreated).ToListAsync();
            return notes;
        }
    }
}