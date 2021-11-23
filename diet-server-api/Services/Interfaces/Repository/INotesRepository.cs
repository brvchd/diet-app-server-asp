using System.Collections.Generic;
using System.Threading.Tasks;
using diet_server_api.DTO.Requests.Doctor;
using diet_server_api.DTO.Responses.Doctor;

namespace diet_server_api.Services.Interfaces.Repository
{
    public interface INotesRepository
    {
        public Task<AddNoteResponse> AddNote(AddNoteRequest request);
        public Task<List<GetNotesResponse>> GetNotes(int idPatient);
    }
}