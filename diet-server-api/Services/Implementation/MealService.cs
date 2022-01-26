using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using diet_server_api.DTO.Requests.KnowledgeBase.Add;
using diet_server_api.DTO.Requests.KnowledgeBase.Update;
using diet_server_api.DTO.Responses.KnowledgeBase.Add;
using diet_server_api.DTO.Responses.KnowledgeBase.Get;
using diet_server_api.DTO.Responses.KnowledgeBase.Search;
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
                Nameofmeal = request.NameOfMeal.Trim(),
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
            .ThenInclude(e => e.ProductParameters)
            .ThenInclude(e => e.IdparameterNavigation)
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
                    CalculatedSize = e.IdproductNavigation.Size * (e.Amount / e.IdproductNavigation.Size),
                    HomeMeasure = e.IdproductNavigation.Homemeasure,
                    HomeMeasureSize = e.IdproductNavigation.Homemeasuresize * (e.Amount / e.IdproductNavigation.Size),
                    Amount = e.Amount,    
                    Params = e.IdproductNavigation.ProductParameters.Select(p => new GetMealsResponse.MealRecipe.Param
                    {
                        CalculatedParamSize = p.Amount * (e.Amount / e.IdproductNavigation.Size),
                        ParamName = p.IdparameterNavigation.Name,
                        ParamMeasureUnit = p.IdparameterNavigation.Measureunit
                    }).ToList()
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

        public async Task<SearchChangedMealResponse> SearchChangedMeal(string mealName, int idDiet)
        {
            if (string.IsNullOrWhiteSpace(mealName))
            {
                throw new InvalidData("Incorrect product name");
            }
            var mealExists = await _dbContext.Meals.AnyAsync(e => e.Nameofmeal.ToLower() == mealName.ToLower().Trim());
            if (!mealExists) throw new NotFound("Meal not found");

            var dietExists = await _dbContext.Diets.AnyAsync(e => e.Iddiet == idDiet);
            if (!dietExists) throw new NotFound("Diet not found");

            var proteinsPerMeal = await _dbContext.Diets.Where(e => e.Iddiet == idDiet).Select(e => e.Protein).FirstAsync();


            var beforeCalculationProteins = await _dbContext.Recipes
            .Include(e => e.IdmealNavigation)
            .Include(e => e.IdproductNavigation)
            .ThenInclude(e => e.ProductParameters)
            .ThenInclude(e => e.IdparameterNavigation)
            .Where(e => e.IdmealNavigation.Nameofmeal.ToLower() == mealName.ToLower().Trim())
            .Select(e => e.IdproductNavigation.ProductParameters
                .Where(e => e.IdparameterNavigation.Name.ToLower() == "proteins")
                .Select(x => x.Amount * (e.Amount / e.IdproductNavigation.Size)
                ).SingleOrDefault()
            ).SumAsync();

            var proteinProportion = decimal.Round(proteinsPerMeal / beforeCalculationProteins, 3);

            var meal = await _dbContext.Meals
            .Include(e => e.Recipes)
            .ThenInclude(e => e.IdproductNavigation)
            .ThenInclude(e => e.ProductParameters)
            .ThenInclude(e => e.IdparameterNavigation)
            .Where(e => e.Nameofmeal.ToLower() == mealName.ToLower().Trim())
            .Select(e => new SearchChangedMealResponse
            {
                IdMeal = e.Idmeal,
                Proportion = proteinProportion,
                NameOfMeal = e.Nameofmeal,
                Description = e.Description,
                CookingURL = e.CookingUrl,
                Recipes = e.Recipes.Select(r => new SearchChangedMealResponse.RecipeProduct
                {
                    IdProduct = r.Idproduct,
                    IdMealRecipe = r.Idrecipe,
                    Name = r.IdproductNavigation.Name,
                    Unit = r.IdproductNavigation.Unit,
                    CalculatedRecipeAmount = decimal.ToDouble(proteinProportion) * decimal.ToDouble(r.Amount),
                    HomeMeasure = r.IdproductNavigation.Homemeasure,
                    HomeMeasureSize = decimal.Round(proteinProportion * r.IdproductNavigation.Homemeasuresize * (r.Amount / r.IdproductNavigation.Size),1),
                    Params = r.IdproductNavigation.ProductParameters.Select(p => new SearchChangedMealResponse.ProductParam
                    {
                        CalculatedParamSize = decimal.ToDouble(proteinProportion) * decimal.ToDouble(p.Amount) * (decimal.ToDouble(r.Amount) / decimal.ToDouble(r.IdproductNavigation.Size)), 
                        ParamName = p.IdparameterNavigation.Name,
                        ParamMeasureUnit = p.IdparameterNavigation.Measureunit
                    }).ToList()
                }).ToList()
            }).SingleOrDefaultAsync();

            return meal;
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
            .ThenInclude(e => e.ProductParameters)
            .ThenInclude(e => e.IdparameterNavigation)
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
                    CalculatedSize = e.IdproductNavigation.Size * (e.Amount / e.IdproductNavigation.Size),
                    HomeMeasure = e.IdproductNavigation.Homemeasure,
                    HomeMeasureSize = e.IdproductNavigation.Homemeasuresize * (e.Amount / e.IdproductNavigation.Size),
                    Amount = e.Amount,    
                    Params = e.IdproductNavigation.ProductParameters.Select(p => new GetMealsResponse.MealRecipe.Param
                    {
                        CalculatedParamSize = p.Amount * (e.Amount / e.IdproductNavigation.Size),
                        ParamName = p.IdparameterNavigation.Name,
                        ParamMeasureUnit = p.IdparameterNavigation.Measureunit
                    }).ToList()
                }).ToList()
            }).OrderBy(e => e.NameOfMeal).ToListAsync();
            if (meals.Count == 0) throw new NotFound("No meals found");
            return meals;
        }

        public async Task UpdateMeal(UpdateMealRequest request)
        {
            var meal = await _dbContext.Meals.FirstOrDefaultAsync(e => e.Idmeal == request.IdMeal);
            if(meal == null) throw new NotFound("Meal not found");
            meal.Nameofmeal = string.IsNullOrWhiteSpace(request.MealName) ? meal.Nameofmeal : request.MealName.Trim();
            meal.Description = string.IsNullOrWhiteSpace(request.Description) ? meal.Description : request.Description;
            meal.CookingUrl = string.IsNullOrWhiteSpace(request.Description) ? meal.CookingUrl : request.CookingURL;
            await _dbContext.SaveChangesAsync();
        }
    }
}
