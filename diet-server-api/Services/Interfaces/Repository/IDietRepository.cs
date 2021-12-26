using System.Threading.Tasks;
using diet_server_api.DTO.Requests.Diet;
using diet_server_api.DTO.Responses.Diet;

namespace diet_server_api.Services.Interfaces.Repository
{
    public interface IDietRepository
    {
        Task<CreateDietResponse> CreateDiet(CreateDietRequest request);
        Task AssignMeal(AssignMealsRequest request);
    }
}