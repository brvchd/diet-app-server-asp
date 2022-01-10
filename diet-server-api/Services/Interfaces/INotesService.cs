using System.Collections.Generic;
using System.Threading.Tasks;
using diet_server_api.DTO.Requests.KnowledgeBase.Add;
using diet_server_api.DTO.Responses.KnowledgeBase.Add;
using diet_server_api.DTO.Responses.KnowledgeBase.Get;

namespace diet_server_api.Services.Interfaces.Repository
{
    public interface INotesService
    {
        Task<AddNoteResponse> AddNote(AddNoteRequest request);
        Task<List<GetNotesResponse>> GetNotes(int idPatient);
    }
}