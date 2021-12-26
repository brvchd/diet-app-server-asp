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

    }
}