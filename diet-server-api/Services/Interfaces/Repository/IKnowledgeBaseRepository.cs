using System.Collections.Generic;
using System.Threading.Tasks;
using diet_server_api.DTO.Requests.Doctor;
using diet_server_api.DTO.Responses.Doctor;

namespace diet_server_api.Services.Interfaces.Repository
{
    public interface IKnowledgeBaseRepository
    {
        Task<AddSupplementResponse> AddSupplement(AddSupplementRequest request);
        Task<AddDiseaseResponse> AddDisease(AddDiseaseRequest request);
        Task<GetSupplementsResponse> GetSupplements(int page);
        Task<GetDiseasesResponse> GetDiseases(int page);
        Task<List<SearchDiseaseResponse>> SearchDisease(string diseaseName);
        Task<List<SearchSupplementResponse>> SearchSupplement(string supplementName);
        Task<AddProductResponse> AddProduct(AddProductRequest request);

    }
}