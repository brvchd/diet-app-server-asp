using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using diet_server_api.DTO.Requests.KnowledgeBase.Add;
using diet_server_api.DTO.Responses.KnowledgeBase;
using diet_server_api.DTO.Responses.KnowledgeBase.Add;
using diet_server_api.DTO.Responses.KnowledgeBase.Get;
using diet_server_api.DTO.Responses.KnowledgeBase.Search;
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
            if (exists) throw new AlreadyExists("Disease already exists");
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

        public async Task<List<SearchDiseaseResponse>> SearchDisease(string diseaseName)
        {
            if (string.IsNullOrWhiteSpace(diseaseName))
            {
                throw new InvalidData("Incorrect parameter diseaseName");
            }
            var disease = await _dbContext.Diseases.Where(e => e.Name.ToLower() == diseaseName.ToLower().Trim()).Select(e => new SearchDiseaseResponse()
            {
                IdDisease = e.Iddisease,
                Name = e.Name,
                Description = e.Description,
                Recommendation = e.Recomendation
            }).ToListAsync();
            if (disease.Count == 0) throw new NotFound("Disease not found");
            return disease;
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

        public async Task<AddProductResponse> AddProduct(AddProductRequest request)
        {
            var productExists = await _dbContext.Products.AnyAsync(e => e.Name.ToLower() == request.Name.ToLower().Trim());
            if (productExists) throw new AlreadyExists("Product already exists");
            var newproduct = new Product()
            {
                Name = request.Name,
                Unit = request.Unit,
                Size = request.Size,
                Homemeasure = request.HomeMeasure,
                Homemeasuresize = request.HomeMeasureSize
            };
            await _dbContext.Products.AddAsync(newproduct);
            foreach (var parameter in request.Parameters)
            {
                var productParameter = new ProductParameter()
                {
                    IdproductNavigation = newproduct,
                    Idparameter = parameter.IdParameter,
                    Amount = parameter.Amount
                };
                await _dbContext.ProductParameters.AddAsync(productParameter);
            }
            await _dbContext.SaveChangesAsync();
            return new AddProductResponse()
            {
                IdProduct = newproduct.Idproduct,
                Name = newproduct.Name
            };
        }

        public async Task<AddParameterResponse> AddParameter(AddParameterRequest request)
        {
            var exists = await _dbContext.Parameters.AnyAsync(e => e.Name.ToLower() == request.Name.ToLower());
            if (exists) throw new AlreadyExists("Parameter already exists");
            var parameter = new Parameter()
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
                Name = e.Name
            }).ToListAsync();
            if (parameters.Count == 0) throw new NotFound("No parameters found");
            return parameters;
        }

        public async Task<List<GetProductsResponse>> GetProducts()
        {
            var products = await _dbContext.Products.Include(e => e.ProductParameters).ThenInclude(e => e.IdparameterNavigation).Select(e => new GetProductsResponse()
            {
                IdProduct = e.Idproduct,
                Name = e.Name,
                Unit = e.Unit,
                Size = e.Size,
                HomeMeasure = e.Homemeasure,
                HomeMeasureSize = e.Homemeasuresize,
                Parameters = e.ProductParameters.Select(e => new GetProductsResponse.Parameter()
                {
                    Name = e.IdparameterNavigation.Name,
                    MeasureUnit = e.IdparameterNavigation.Measureunit,
                    Amount = e.Amount
                }).ToList()
            }).ToListAsync();
            if (products.Count == 0) throw new NotFound("No products found");
            return products;
        }
    }
}