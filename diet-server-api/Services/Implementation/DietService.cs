using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using diet_server_api.DTO.Requests.Diet;
using diet_server_api.DTO.Responses.Diet;
using diet_server_api.DTO.Responses.KnowledgeBase.Get;
using diet_server_api.Exceptions;
using diet_server_api.Models;
using diet_server_api.Services.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace diet_server_api.Services.Implementation.Repository
{
    public class DietService : IDietService
    {
        private readonly mdzcojxmContext _dbContext;

        public DietService(mdzcojxmContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AssignMeal(AssignMealsRequest request)
        {
            var dietExists = await _dbContext.Diets.AnyAsync(e => e.Iddiet == request.IdDiet);
            if (!dietExists) throw new NotFound("Diet not found");
            var dayExists = await _dbContext.Days.AnyAsync(e => e.Dietiddiet == request.IdDiet && e.Daynumber == request.DayNumber);
            if (dayExists) throw new AlreadyExists("Day was already added");

            var day = new Day()
            {
                Daynumber = request.DayNumber,
                Dietiddiet = request.IdDiet
            };

            await _dbContext.Days.AddAsync(day);

            if (dayExists) throw new AlreadyExists("Day already exists");

            foreach (var meal in request.Meals)
            {
                var mealRecipes = await _dbContext.Recipes.Where(e => e.Idmeal == meal.IdMeal).Select(e => new { RecipeId = e.Idrecipe }).ToListAsync();

                var mealTake = new Mealtake()
                {
                    IddayNavigation = day,
                    Time = meal.Time,
                    Proportion = meal.Proportion
                };
                await _dbContext.Mealtakes.AddAsync(mealTake);
                foreach (var recipe in mealRecipes)
                {
                    var individualRecipe = new Individualrecipe()
                    {
                        Idrecipe = recipe.RecipeId,
                        IdmealtakeNavigation = mealTake,
                    };
                    await _dbContext.Individualrecipes.AddAsync(individualRecipe);
                }
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task<CreateDietResponse> CreateDiet(CreateDietRequest request)
        {
            var patientexists = await _dbContext.Patients.AnyAsync(e => e.Iduser == request.IdPatient && e.Ispending == false);
            if (!patientexists) throw new NotFound("Patient not found");
            var activeAccount = await _dbContext.Users.AnyAsync(e => e.Iduser == request.IdPatient && e.Isactive == true);
            if (!activeAccount) throw new NotActive("Account is not active");
            var diet = new Diet()
            {
                Idpatient = request.IdPatient,
                Name = request.Name,
                Description = request.Description,
                Datefrom = request.DateFrom,
                Dateto = request.DateTo,
                Dailymeals = request.DailyMeals,
                Protein = request.Protein
            };
            await _dbContext.Diets.AddAsync(diet);

            foreach (var suppl in request.Supplements)
            {
                var exists = await _dbContext.Dietsuppliments.AnyAsync(e => e.IddietNavigation == diet && e.Iddietsuppliment == suppl.IdSupplement);
                if (exists) throw new AlreadyExists("Supplement already exists");
                var suppldiet = new Dietsuppliment()
                {
                    IddietNavigation = diet,
                    Idsuppliment = suppl.IdSupplement,
                    Description = suppl.DietSupplDescription
                };
                await _dbContext.Dietsuppliments.AddAsync(suppldiet);
            }

            await _dbContext.SaveChangesAsync();
            return new CreateDietResponse
            {
                IdDiet = diet.Iddiet,
                Name = diet.Name
            };
        }

        public async Task<GetDietDaysResponse> GetDays(int idDiet)
        {
            var diet = await _dbContext.Diets.FirstOrDefaultAsync(e => e.Iddiet == idDiet);
            if (diet == null) throw new NotFound("Diet not found");
            var dietDays = (int)diet.Dateto.Subtract(diet.Datefrom).TotalDays + 1;
            var daysFilled = await _dbContext.Days.Where(e => e.Dietiddiet == idDiet).CountAsync();
            var daysFilledNumbers = await _dbContext.Days.Where(e => e.Dietiddiet == idDiet).Select(e => e.Daynumber).ToListAsync();
            return new GetDietDaysResponse
            {
                Days = dietDays,
                TotalMeals = diet.Dailymeals,
                Proteins = diet.Protein,
                DaysFilled = daysFilled,
                DaysNumberFilled = daysFilledNumbers
            };
        }

        public async Task<List<GetDietMealsResponse>> GetDietMeals(int idDiet)
        {
            var dietExist = await _dbContext.Diets.AnyAsync(e => e.Iddiet == idDiet);
            if (!dietExist) throw new NotFound("Diet not found");
            var daysFilled = await _dbContext.Days.AnyAsync(e => e.Dietiddiet == idDiet);
            if (!daysFilled) throw new NotFound("Diet days are not filled yet");

            var result = await _dbContext.Days.Include(e => e.Mealtakes)
            .ThenInclude(e => e.Individualrecipes)
            .ThenInclude(e => e.IdrecipeNavigation)
            .ThenInclude(e => e.IdmealNavigation)
            .Select(e => new GetDietMealsResponse
            {
                IdDay = e.Idday,
                DayNumber = e.Daynumber,
                PatientReport = e.Patientreport,
                Meals = e.Mealtakes.Select(e => new DayMealTake
                {
                    Time = e.Time,
                    IsFollowed = e.Isfollowed,
                    Proportion = e.Proportion,
                    NameOfMeal = e.Individualrecipes.First().IdrecipeNavigation.IdmealNavigation.Nameofmeal,
                    Description = e.Individualrecipes.First().IdrecipeNavigation.IdmealNavigation.Description,
                    Cooking_URL = e.Individualrecipes.First().IdrecipeNavigation.IdmealNavigation.CookingUrl,
                    Recipes = e.Individualrecipes.Select(e => new MealRecipe
                    {
                        IdProduct = e.Idrecipe,
                        Amount = e.IdrecipeNavigation.Amount,
                    })
                })
            }).ToListAsync();


            return result;
        }

        public async Task<List<GetPatientDietResponse>> GetPatientDiets(int idPatient)
        {
            var exists = await _dbContext.Patients.AnyAsync(e => e.Iduser == idPatient && e.Ispending == false);
            if (!exists) throw new NotFound("Patient not found");
            var accountIsActive = await _dbContext.Users.AnyAsync(e => e.Iduser == idPatient && e.Isactive == true);
            if (!accountIsActive) throw new NotActive("Account is not active");
            var diets = await _dbContext.Diets.Where(e => e.Idpatient == idPatient).Select(e => new GetPatientDietResponse()
            {
                IdDiet = e.Iddiet,
                Name = e.Name,
                Description = e.Description,
                DailyMeals = e.Dailymeals,
                Protein = e.Protein,
                DateFrom = e.Datefrom,
                DateTo = e.Dateto,
                ChangesDate = e.Changesdate
            }).ToListAsync();

            return diets;
        }
    }
}