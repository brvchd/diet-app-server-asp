using System.Linq;
using System.Threading.Tasks;
using diet_server_api.DTO.Requests.Diet;
using diet_server_api.DTO.Responses.Diet;
using diet_server_api.Exceptions;
using diet_server_api.Models;
using diet_server_api.Services.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace diet_server_api.Services.Implementation.Repository
{
    public class DietRepository : IDietRepository             
    {   
        private readonly mdzcojxmContext _dbContext;

        public DietRepository(mdzcojxmContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AssignMeal(AssignMealsRequest request)
        {
            var dietExists = await _dbContext.Diets.AnyAsync(e => e.Iddiet == request.IdDiet);
            if(!dietExists) throw new NotFound("Diet not found");
            var dayExists = await _dbContext.Days.AnyAsync(e => e.Dietiddiet == request.IdDiet && e.Daynumber == request.DayNumber);
            if(dayExists) throw new AlreadyExists("Day was already added");

            var day = new Day()
            {
                Daynumber = request.DayNumber,
                Dietiddiet = request.IdDiet
            };

            await _dbContext.Days.AddAsync(day);

            if(dayExists) throw new AlreadyExists("Day already exists");

            foreach(var meal in request.Meals)
            {
                var mealRecipes = await _dbContext.Recipes.Where(e => e.Idmeal == meal.IdMeal).Select(e => new {RecipeId = e.Idrecipe}).ToListAsync();

                var mealTake = new Mealtake() 
                {
                    IddayNavigation = day,
                    Time =  meal.Time,
                    Proportion = meal.Proportion
                };
                await _dbContext.Mealtakes.AddAsync(mealTake);
                foreach(var recipe in mealRecipes)
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
            if(diet == null) throw new NotFound("Diet not found");
            var dietDays = (int)diet.Dateto.Subtract(diet.Datefrom).TotalDays + 1;
            var daysFilled = await _dbContext.Days.Where(e => e.Dietiddiet == idDiet).CountAsync();
            return new GetDietDaysResponse{
                Days = dietDays,
                TotalMeals = diet.Dailymeals,
                Proteins = diet.Protein,
                DaysFilled = daysFilled 
            };
        }
    }
}