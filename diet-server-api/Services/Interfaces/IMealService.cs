using System.Collections.Generic;
using System.Threading.Tasks;
using diet_server_api.DTO.Requests.KnowledgeBase.Add;
using diet_server_api.DTO.Requests.KnowledgeBase.Update;
using diet_server_api.DTO.Responses.KnowledgeBase.Add;
using diet_server_api.DTO.Responses.KnowledgeBase.Get;
using diet_server_api.DTO.Responses.KnowledgeBase.Search;

namespace diet_server_api.Services.Interfaces.Repository
{
    public interface IMealService
    {
        Task<AddMealResponse> AddMeal(AddMealRequest request);
        Task<GetMealsResponse> GetMeals(int page);
        Task<List<GetMealsResponse.MealRecipe>> SearchMeal(string mealName);
        Task<SearchChangedMealResponse> SearchChangedMeal(string mealName, int idDiet);
        Task UpdateMeal(UpdateMealRequest request);
    }
}