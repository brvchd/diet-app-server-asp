using System.Collections.Generic;
using System.Threading.Tasks;
using diet_server_api.DTO.Requests.KnowledgeBase.Add;
using diet_server_api.DTO.Requests.KnowledgeBase.Update;
using diet_server_api.DTO.Responses.KnowledgeBase;
using diet_server_api.DTO.Responses.KnowledgeBase.Add;
using diet_server_api.DTO.Responses.KnowledgeBase.Get;
using diet_server_api.DTO.Responses.KnowledgeBase.Update;

namespace diet_server_api.Services.Interfaces.Repository
{
    public interface IDiseaseService
    {
        Task<AddDiseaseResponse> AddDisease(AddDiseaseRequest request);
        Task AssignDisease(AssignDiseaseRequest request);
        Task DeleteAssignedDisease(int idDiseasePatient);
        Task<GetDiseasesResponse> GetDiseases(int page);
        Task<List<GetPatientDiseasesResponse>> GetPatientDiseases(int patientId);
        Task<List<SearchDiseaseResponse>> SearchDisease(string diseaseName);
        Task<UpdateDiseaseResponse> UpdateDisease(UpdateDiseaseRequest request);
        Task UpdatePatientDisease(UpdatePatientDiseaseRequest request);  
    }
}