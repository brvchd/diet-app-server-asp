using System.Collections.Generic;
using System.Threading.Tasks;
using diet_server_api.DTO.Requests.Diet;
using diet_server_api.DTO.Responses.Diet;

namespace diet_server_api.Services.Interfaces.Repository
{
    public interface IDietService
    {
        Task<CreateDietResponse> CreateDiet(CreateDietRequest request);
        Task AssignMeal(AssignMealsRequest request);
        Task<GetDietDaysResponse> GetDays(int idDiet);
        Task<List<GetPatientDietResponse>> GetPatientDiets(int idPatient);
        Task<List<GetDietMealsResponse>> GetDietMeals(int idDiet);
    }
}