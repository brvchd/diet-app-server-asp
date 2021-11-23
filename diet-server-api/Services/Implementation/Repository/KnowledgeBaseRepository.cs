using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using diet_server_api.DTO.Requests.Doctor;
using diet_server_api.DTO.Responses.Doctor;
using diet_server_api.Exceptions;
using diet_server_api.Models;
using diet_server_api.Services.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace diet_server_api.Services.Implementation.Repository
{
    public class KnowledgeBaseRepository : IKnowledgeBaseRepository
    {
        private readonly mdzcojxmContext _dbContext;

        public KnowledgeBaseRepository(mdzcojxmContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AddDiseaseResponse> AddDisease(AddDiseaseRequest request)
        {
            var exists = await _dbContext.Diseases.AnyAsync(e => e.Name == request.Name);
            if (exists) throw new DiseaseAlreadyExists();
            var disease = new Disease()
            {
                Name = request.Name,
                Description = request.Description,
                Recomendation = request.Recomendation
            };
            await _dbContext.Diseases.AddAsync(disease);
            await _dbContext.SaveChangesAsync();
            return new AddDiseaseResponse()
            {
                IdDisease = disease.Iddisease,
                Name = disease.Name,
                Description = disease.Description
            };
        }

        public async Task<AddSupplementResponse> AddSupplement(AddSupplementRequest request)
        {
            var exists = await _dbContext.Supplements.AnyAsync(e => e.Name == request.Name);
            if (exists) throw new SupplementAlreadyExists();
            var supplement = new Supplement()
            {
                Name = request.Name,
                Description = request.Description
            };
            await _dbContext.Supplements.AddAsync(supplement);
            await _dbContext.SaveChangesAsync();
            return new AddSupplementResponse()
            {
                IdSupplement = supplement.Idsuppliment,
                Name = supplement.Name,
                Description = supplement.Description
            };
        }

        public async Task<List<GetSupplementsResponse>> GetSupplements()
        {
            var supplements = await _dbContext.Supplements.Select(e => new GetSupplementsResponse
            {
                SupplementName = e.Name,
                Description = e.Description
            }).ToListAsync();
            return supplements;
        }
    }
}