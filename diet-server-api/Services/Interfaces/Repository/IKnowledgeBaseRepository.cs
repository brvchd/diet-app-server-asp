using System.Collections.Generic;
using System.Threading.Tasks;
using diet_server_api.DTO.Requests.Doctor;
using diet_server_api.DTO.Responses.Doctor;

namespace diet_server_api.Services.Interfaces.Repository
{
    public interface IKnowledgeBaseRepository
    {
        public Task<AddSupplementResponse> AddSupplement(AddSupplementRequest request);
        public Task<AddDiseaseResponse> AddDisease(AddDiseaseRequest request);
        public Task<List<GetSupplementsResponse>> GetSupplements();
    }
}