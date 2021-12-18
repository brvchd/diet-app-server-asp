using System.Collections.Generic;
using System.Threading.Tasks;
using diet_server_api.DTO.Requests.KnowledgeBase.Add;
using diet_server_api.DTO.Requests.KnowledgeBase.Update;
using diet_server_api.DTO.Responses.KnowledgeBase.Add;
using diet_server_api.DTO.Responses.KnowledgeBase.Get;
using diet_server_api.DTO.Responses.KnowledgeBase.Search;
using diet_server_api.DTO.Responses.KnowledgeBase.Update;

namespace diet_server_api.Services.Interfaces.Repository
{
    public interface ISupplementRepository
    {
        Task<AddSupplementResponse> AddSupplement(AddSupplementRequest request);
        Task<GetSupplementsResponse> GetSupplements(int page);
        Task<List<SearchSupplementResponse>> SearchSupplement(string supplementName);
        Task<UpdateSupplementResponse> UpdateSupplement(UpdateSupplementRequest request);
    }
}