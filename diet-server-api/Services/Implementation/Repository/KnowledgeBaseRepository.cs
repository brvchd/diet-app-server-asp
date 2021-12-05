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

        public async Task<GetSupplementsResponse> GetSupplements(int page = 1)
        {
            if (page < 1) page = 1;
            int pageSize = 8;
            var rows = await _dbContext.Supplements.CountAsync();
            var supplements = await _dbContext.Supplements.OrderBy(e => e.Name).Skip((page - 1) * pageSize).Take(pageSize).Select(e => new GetSupplementsResponse.Supplement
            {
                IdSupplement = e.Idsuppliment,
                SupplementName = e.Name,
                Description = e.Description
            }).ToListAsync();

            return new GetSupplementsResponse()
            {
                Supplements = supplements,
                PageNumber = page,
                PageSize = pageSize,
                TotalRows = rows
            };
        }
        public async Task<GetDiseasesResponse> GetDiseases(int page = 1)
        {
            if (page < 1) page = 1;
            int pageSize = 8;
            var rows = await _dbContext.Diseases.CountAsync();
            var diseases = await _dbContext.Diseases.OrderBy(e => e.Name).Skip((page - 1) * pageSize).Take(pageSize).Select(e => new GetDiseasesResponse.Disease
            {
                IdDisease = e.Iddisease,
                Name = e.Name,
                Description = e.Description,
                Recomendation = e.Recomendation
            }).ToListAsync();

            return new GetDiseasesResponse()
            {
                Diseases = diseases,
                PageNumber = page,
                PageSize = pageSize,
                TotalRows = rows
            };
        }

        public async Task<SearchDiseaseResponse> SearchDisease(string diseaseName)
        {
            if (string.IsNullOrWhiteSpace(diseaseName))
            {
                throw new IncorrectParameter();
            }
            var disease = await _dbContext.Diseases.FirstOrDefaultAsync(e => e.Name.ToLower() == diseaseName.ToLower().Trim());
            if (disease != null)
            {
                return new SearchDiseaseResponse()
                {
                    IdDisease = disease.Iddisease,
                    DiseaseName = disease.Name,
                    Description = disease.Description,
                    Recommendation = disease.Recomendation
                };
            }
            else
            {
                throw new DiseaseNotFound();
            }
        }

        public async Task<SearchSupplementResponse> SearchSupplement(string supplementName)
        {
            if (string.IsNullOrWhiteSpace(supplementName))
            {
                throw new IncorrectParameter();
            }
            var supplement = await _dbContext.Supplements.FirstOrDefaultAsync(e => e.Name.ToLower() == supplementName.ToLower().Trim());
            if (supplement != null)
            {
                return new SearchSupplementResponse()
                {
                    IdSupplement = supplement.Idsuppliment,
                    SupplementName = supplement.Name,
                    Description = supplement.Description
                };
            }
            else
            {
                throw new SupplementNotFound();
            }
        }
    }
}