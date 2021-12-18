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
    public class ParameterRepository : IParameterRepository
    {
        private readonly mdzcojxmContext _dbContext;

        public ParameterRepository(mdzcojxmContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AddParameterResponse> AddParameter(AddParameterRequest request)
        {
            var exists = await _dbContext.Parameters.AnyAsync(e => e.Name.ToLower() == request.Name.ToLower());
            if (exists) throw new AlreadyExists("Parameter already exists");
            var parameter = new Parameter
            {
                Name = request.Name,
                Measureunit = request.MeasureUnit
            };
            await _dbContext.Parameters.AddAsync(parameter);
            await _dbContext.SaveChangesAsync();
            return new AddParameterResponse
            {
                idParam = parameter.Idparameter,
                Name = parameter.Name
            };
        }

        public async Task<List<GetParametersResponse>> GetParameters()
        {
            var parameters = await _dbContext.Parameters.Select(e => new GetParametersResponse()
            {
                IdParameter = e.Idparameter,
                Name = e.Name,
                MeasureUnit = e.Measureunit
            }).OrderBy(e => e.Name).ToListAsync();
            if (parameters.Count == 0) throw new NotFound("No parameters found");
            return parameters;
        }
    }
}