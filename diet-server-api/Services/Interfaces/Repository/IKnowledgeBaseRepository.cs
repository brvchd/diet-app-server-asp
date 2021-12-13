using System.Collections.Generic;
using System.Threading.Tasks;
using diet_server_api.DTO.Requests.KnowledgeBase.Add;
using diet_server_api.DTO.Requests.KnowledgeBase.Update;
using diet_server_api.DTO.Responses.KnowledgeBase;
using diet_server_api.DTO.Responses.KnowledgeBase.Add;
using diet_server_api.DTO.Responses.KnowledgeBase.Get;
using diet_server_api.DTO.Responses.KnowledgeBase.Search;
using diet_server_api.DTO.Responses.KnowledgeBase.Update;

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
        Task<AddParameterResponse> AddParameter(AddParameterRequest request);
        Task<List<GetParametersResponse>> GetParameters();
        Task<GetProductsResponse> GetProducts(int page);
        Task<UpdateDiseaseResponse> UpdateDisease(UpdateDiseaseRequest request);
        Task<UpdateSupplementResponse> UpdateSupplement(UpdateSupplementRequest request);
        Task<List<GetProductsResponse.Product>> SearchProduct(string product);


    }
}