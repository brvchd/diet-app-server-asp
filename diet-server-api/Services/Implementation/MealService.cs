using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using diet_server_api.DTO.Requests.KnowledgeBase.Add;
using diet_server_api.DTO.Responses.KnowledgeBase.Add;
using diet_server_api.DTO.Responses.KnowledgeBase.Get;
using diet_server_api.Exceptions;
using diet_server_api.Models;
using diet_server_api.Services.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace diet_server_api.Services.Implementation.Repository
{
    public class MealService : IMealService
    {
        private readonly mdzcojxmContext _dbContext;

        public MealService(mdzcojxmContext dbContext)
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

        public async Task<GetMealsResponse> GetMeals(int page = 1)
        {
            if (page < 1) page = 1;
            int pageSize = 12;
            var rows = await _dbContext.Products.CountAsync();

            var meals = await _dbContext.Meals
            .Include(e => e.Recipes)
            .ThenInclude(e => e.IdproductNavigation)
            .OrderBy(e => e.Nameofmeal)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(e => new GetMealsResponse.MealRecipe()
            {
                IdMeal = e.Idmeal,
                NameOfMeal = e.Nameofmeal,
                Description = e.Description,
                CookingURL = e.CookingUrl,
                Products = e.Recipes.Select(e => new GetMealsResponse.MealRecipe.RecipeProduct()
                {
                    IdProduct = e.Idproduct,
                    IdMealRecipe = e.Idrecipe,
                    Name = e.IdproductNavigation.Name,
                    Unit = e.IdproductNavigation.Unit,
                    Size = e.IdproductNavigation.Size,
                    HomeMeasure = e.IdproductNavigation.Homemeasure,
                    HomeMeasureSize = e.IdproductNavigation.Homemeasuresize,
                    Amount = e.Amount,
                }).ToList()
            }).ToListAsync();
            if (meals.Count == 0) throw new NotFound("No products found");
            return new GetMealsResponse
            {
                PageNumber = page,
                TotalRows = rows,
                PageSize = pageSize,
                Meals = meals
            };
        }

        public async Task<List<GetMealsResponse.MealRecipe>> SearchMeal(string mealName)
        {
            if (string.IsNullOrWhiteSpace(mealName))
            {
                throw new InvalidData("Incorrect product name");
            }
            var meals = await _dbContext.Meals
            .Include(e => e.Recipes)
            .ThenInclude(e => e.IdproductNavigation)
            .Where(e => e.Nameofmeal.ToLower() == mealName.ToLower().Trim())
            .Select(e => new GetMealsResponse.MealRecipe()
            {
                IdMeal = e.Idmeal,
                NameOfMeal = e.Nameofmeal,
                Description = e.Description,
                CookingURL = e.CookingUrl,
                Products = e.Recipes.Select(e => new GetMealsResponse.MealRecipe.RecipeProduct()
                {
                    IdProduct = e.Idproduct,
                    IdMealRecipe = e.Idrecipe,
                    Name = e.IdproductNavigation.Name,
                    Unit = e.IdproductNavigation.Unit,
                    Size = e.IdproductNavigation.Size,
                    HomeMeasure = e.IdproductNavigation.Homemeasure,
                    HomeMeasureSize = e.IdproductNavigation.Homemeasuresize,
                    Amount = e.Amount,
                }).ToList()
            }).OrderBy(e => e.NameOfMeal).ToListAsync();
            if (meals.Count == 0) throw new NotFound("No meals found");
            return meals;
        }
    }
}
