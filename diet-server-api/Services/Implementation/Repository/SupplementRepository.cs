using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using diet_server_api.DTO.Requests.KnowledgeBase.Add;
using diet_server_api.DTO.Requests.KnowledgeBase.Update;
using diet_server_api.DTO.Responses.KnowledgeBase.Add;
using diet_server_api.DTO.Responses.KnowledgeBase.Get;
using diet_server_api.DTO.Responses.KnowledgeBase.Search;
using diet_server_api.DTO.Responses.KnowledgeBase.Update;
using diet_server_api.Exceptions;
using diet_server_api.Models;
using diet_server_api.Services.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace diet_server_api.Services.Implementation.Repository
{
    public class SupplementRepository : ISupplementRepository
    {
        private readonly mdzcojxmContext _dbContext;

        public SupplementRepository(mdzcojxmContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AddSupplementResponse> AddSupplement(AddSupplementRequest request)
        {
            var exists = await _dbContext.Supplements.AnyAsync(e => e.Name == request.Name);
            if (exists) throw new AlreadyExists("Supplement already exists");
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

        public async Task<GetSupplementsResponse> GetSupplements(int page)
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

        public async Task<List<SearchSupplementResponse>> SearchSupplement(string supplementName)
        {
            if (string.IsNullOrWhiteSpace(supplementName))
            {
                throw new InvalidData("Incorrect supplementName");
            }
            var supplement = await _dbContext.Supplements.Where(e => e.Name.ToLower() == supplementName.ToLower().Trim()).Select(e => new SearchSupplementResponse()
            {
                IdSupplement = e.Idsuppliment,
                SupplementName = e.Name,
                Description = e.Description
            }).ToListAsync();
            if (supplement.Count == 0) throw new NotFound("Supplement not found");
            return supplement;
        }

        public async Task<UpdateSupplementResponse> UpdateSupplement(UpdateSupplementRequest request)
        {
            var supplement = await _dbContext.Supplements.FirstOrDefaultAsync(e => e.Idsuppliment == request.IdSupplement);
            if (supplement == null) throw new NotFound("Supplement not found");
            supplement.Name = string.IsNullOrWhiteSpace(request.Name) ? supplement.Name : request.Name;
            supplement.Description = string.IsNullOrWhiteSpace(request.Description) ? supplement.Description : request.Description;

            await _dbContext.SaveChangesAsync();
            return new UpdateSupplementResponse
            {
                Name = supplement.Name,
                Description = supplement.Description
            };
        }
    }
}