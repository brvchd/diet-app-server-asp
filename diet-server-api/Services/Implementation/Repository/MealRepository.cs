using System.Threading.Tasks;
using diet_server_api.DTO.Requests.KnowledgeBase.Add;
using diet_server_api.DTO.Responses.KnowledgeBase.Add;
using diet_server_api.Exceptions;
using diet_server_api.Models;
using diet_server_api.Services.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace diet_server_api.Services.Implementation.Repository
{
    public class MealRepository : IMealRepository
    {
        private readonly mdzcojxmContext _dbContext;

        public MealRepository(mdzcojxmContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AddMealResponse> AddMeal(AddMealRequest request)
        {
            var mealExists = await _dbContext.Meals.AnyAsync(e => e.Nameofmeal.ToLower() == request.NameOfMeal.ToLower().Trim());
            if (mealExists) throw new AlreadyExists("Meal with such name already exists");
            var meal = new Meal()
            {
                Nameofmeal = request.NameOfMeal,
                Description = request.Description,
                CookingUrl = request.CookingURL
            };
            await _dbContext.Meals.AddAsync(meal);
            foreach (var recipe in request.Recipes)
            {
                var exists = await _dbContext.Products.AnyAsync(e => e.Idproduct == recipe.IdProduct);
                if (!exists) throw new NotFound("Product not found");
                var newrecipe = new Recipe()
                {
                    IdmealNavigation = meal,
                    Idproduct = recipe.IdProduct,
                    Amount = recipe.Amount
                };
                await _dbContext.Recipes.AddAsync(newrecipe);
            }
            await _dbContext.SaveChangesAsync();
            return new AddMealResponse
            {
                IdMeal = meal.Idmeal,
                MealName = meal.Nameofmeal
            };
        }
    }
}