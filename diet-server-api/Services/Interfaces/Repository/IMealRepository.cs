using System.Threading.Tasks;
using diet_server_api.DTO.Requests.KnowledgeBase.Add;
using diet_server_api.DTO.Responses.KnowledgeBase.Add;

namespace diet_server_api.Services.Interfaces.Repository
{
    public interface IMealRepository
    {
        Task<AddMealResponse> AddMeal(AddMealRequest request);
    }
}