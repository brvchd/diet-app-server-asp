using System.Collections.Generic;
using System.Threading.Tasks;
using diet_server_api.DTO.Requests.KnowledgeBase.Add;
using diet_server_api.DTO.Responses.KnowledgeBase.Add;
using diet_server_api.DTO.Responses.KnowledgeBase.Get;

namespace diet_server_api.Services.Interfaces.Repository
{
    public interface IParameterRepository
    {
        Task<AddParameterResponse> AddParameter(AddParameterRequest request);
        Task<List<GetParametersResponse>> GetParameters();
    }
}