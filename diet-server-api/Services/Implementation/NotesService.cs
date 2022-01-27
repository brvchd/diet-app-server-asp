using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using diet_server_api.DTO.Requests.KnowledgeBase.Add;
using diet_server_api.DTO.Responses.KnowledgeBase.Add;
using diet_server_api.DTO.Responses.KnowledgeBase.Get;
using diet_server_api.Exceptions;
using diet_server_api.Helpers;
using diet_server_api.Models;
using diet_server_api.Services.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace diet_server_api.Services.Implementation.Repository
{
    public class NotesService : INotesService
    {
        private readonly mdzcojxmContext _dbContext;

        public NotesService(mdzcojxmContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AddNoteResponse> AddNote(AddNoteRequest request)
        {
            var patientExists = await _dbContext.Patients.AnyAsync(e => e.Iduser == request.IdPatient && e.Ispending == false);
            if (!patientExists) throw new NotFound("Patient not found!");
            var doctorExists = await _dbContext.Doctors.AnyAsync(e => e.Iduser == request.IdDoctor);
            if (!doctorExists) throw new NotFound("Doctor not found!");

            var note = new Note()
            {
                Iddoctor = request.IdDoctor,
                Idpatient = request.IdPatient,
                Message = request.Note,
                Dateofnote = TimeConverter.GetCurrentPolishTime()
            };

            await _dbContext.Notes.AddAsync(note);
            await _dbContext.SaveChangesAsync();

            return new AddNoteResponse()
            {
                IdNote = note.Idnote,
                Message = note.Message
            };
        }

        public async Task<List<GetNotesResponse>> GetNotes(int idPatient)
        {
            var patientExists = await _dbContext.Patients.AnyAsync(e => e.Iduser == idPatient && e.Ispending == false);
            if (!patientExists) throw new NotFound("Patient not found!");
            var accountIsNotActive = await _dbContext.Users.AnyAsync(e => e.Iduser == idPatient && e.Isactive == false);
            if (accountIsNotActive) throw new NotActive("Account is not active");
            var notes = await _dbContext.Notes.Include(e => e.IddoctorNavigation).ThenInclude(e => e.IduserNavigation).Where(e => e.Idpatient == idPatient).Select(e => new GetNotesResponse
            {
                IdNote = e.Idnote,
                CreatedBy = e.IddoctorNavigation.IduserNavigation.Firstname + " " + e.IddoctorNavigation.IduserNavigation.Lastname,
                NoteText = e.Message,
                NoteCreated = e.Dateofnote
            }).OrderBy(e => e.NoteCreated).ToListAsync();
            return notes;
        }
    }
}