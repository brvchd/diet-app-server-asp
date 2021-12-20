using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using diet_server_api.DTO.Requests.KnowledgeBase.Add;
using diet_server_api.DTO.Requests.KnowledgeBase.Update;
using diet_server_api.DTO.Responses.Doctor.Update;
using diet_server_api.DTO.Responses.KnowledgeBase.Add;
using diet_server_api.DTO.Responses.KnowledgeBase.Get;
using diet_server_api.Exceptions;
using diet_server_api.Models;
using diet_server_api.Services.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace diet_server_api.Services.Implementation.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly mdzcojxmContext _dbContext;

        public ProductRepository(mdzcojxmContext dbContext)
        {
            _dbContext = dbContext;
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
                var exists = await _dbContext.Parameters.AnyAsync(e => e.Idparameter == parameter.IdParameter);
                if (!exists) throw new NotFound("Parameter does not exist");
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

        public async Task<List<GetProductParametersResponse>> GetProductParams(int IdProduct)
        {
            var exists = await _dbContext.Products.AnyAsync(e => e.Idproduct == IdProduct);
            if (!exists) throw new NotFound("Product not found");
            var products = await _dbContext.Products.Include(e => e.ProductParameters).ThenInclude(e => e.IdparameterNavigation).Where(e => e.Idproduct == IdProduct).Select(e => new GetProductParametersResponse()
            {
                IdProduct = e.Idproduct,
                Name = e.Name,
                Unit = e.Unit,
                Size = e.Size,
                HomeMeasure = e.Homemeasure,
                HomeMeasureSize = e.Homemeasuresize,
                Parameters = e.ProductParameters.Select(e => new GetProductsResponse.Parameter()
                {
                    IdParameter = e.Idparameter,
                    Name = e.IdparameterNavigation.Name,
                    MeasureUnit = e.IdparameterNavigation.Measureunit,
                    Amount = e.Amount
                }).ToList()
            }).ToListAsync();

            return products;

        }

        public async Task<GetProductsResponse> GetProducts(int page)
        {
            if (page < 1) page = 1;
            int pageSize = 12;
            var rows = await _dbContext.Products.CountAsync();

            var products = await _dbContext.Products.Include(e => e.ProductParameters).ThenInclude(e => e.IdparameterNavigation).OrderBy(e => e.Name).Skip((page - 1) * pageSize).Take(pageSize).Select(e => new GetProductsResponse.Product()
            {
                IdProduct = e.Idproduct,
                Name = e.Name,
                Unit = e.Unit,
                Size = e.Size,
                HomeMeasure = e.Homemeasure,
                HomeMeasureSize = e.Homemeasuresize,
                Parameters = e.ProductParameters.Select(e => new GetProductsResponse.Parameter()
                {
                    IdParameter = e.Idparameter,
                    Name = e.IdparameterNavigation.Name,
                    MeasureUnit = e.IdparameterNavigation.Measureunit,
                    Amount = e.Amount
                }).ToList()
            }).ToListAsync();
            if (products.Count == 0) throw new NotFound("No products found");
            return new GetProductsResponse()
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalRows = rows,
                Products = products
            };
        }

        public async Task<List<GetProductsResponse.Product>> SearchProduct(string product)
        {
            if (string.IsNullOrWhiteSpace(product))
            {
                throw new InvalidData("Incorrect product name");
            }

            var products = await _dbContext.Products.Include(e => e.ProductParameters).ThenInclude(e => e.IdparameterNavigation).Where(e => e.Name.ToLower() == product.ToLower().Trim()).Select(e => new GetProductsResponse.Product()
            {
                IdProduct = e.Idproduct,
                Name = e.Name,
                Unit = e.Unit,
                Size = e.Size,
                HomeMeasure = e.Homemeasure,
                HomeMeasureSize = e.Homemeasuresize,
                Parameters = e.ProductParameters.Select(e => new GetProductsResponse.Parameter()
                {
                    IdParameter = e.Idparameter,
                    Name = e.IdparameterNavigation.Name,
                    MeasureUnit = e.IdparameterNavigation.Measureunit,
                    Amount = e.Amount
                }).ToList()
            }).ToListAsync();

            if (products.Count == 0) throw new NotFound("No products found");
            return products;
        }

        public async Task<UpdateProductResponse> UpdateProduct(UpdateProductRequest request)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(e => e.Idproduct == request.ProductId);
            if (product == null) throw new NotFound("Product not found");
            if (request.Name != null)
            {
                var prodNameExists = await _dbContext.Products.FirstOrDefaultAsync(e => e.Name.ToLower() == request.Name.ToLower().Trim());
                if (prodNameExists != null && prodNameExists.Idproduct != product.Idproduct) throw new AlreadyExists("Product with such name already exists");
            }
            var prodParams = await _dbContext.ProductParameters.Where(e => e.Idproduct == request.ProductId).ToListAsync();
            product.Name = string.IsNullOrWhiteSpace(request.Name) ? product.Name : request.Name;
            product.Unit = string.IsNullOrWhiteSpace(request.Unit) ? product.Unit : request.Unit;
            product.Size = request.Size <= 0 ? product.Size : request.Size;
            product.Homemeasure = string.IsNullOrWhiteSpace(request.HomeMeasure) ? product.Homemeasure : request.HomeMeasure;
            product.Homemeasuresize = request.Size <= 0 ? product.Homemeasuresize : request.HomeMeasureSize;

            foreach (var param in prodParams)
            {
                _dbContext.ProductParameters.Remove(param);
            }
            foreach (var parameter in request.Parameters)
            {
                var exists = await _dbContext.Parameters.AnyAsync(e => e.Idparameter == parameter.IdParameter);
                if (!exists) throw new NotFound("Parameter does not exist");
                var productParameter = new ProductParameter()
                {
                    IdproductNavigation = product,
                    Idparameter = parameter.IdParameter,
                    Amount = parameter.Amount
                };
                await _dbContext.ProductParameters.AddAsync(productParameter);
            }
            await _dbContext.SaveChangesAsync();
            var newparameters = await _dbContext.ProductParameters.Where(e => e.Idproduct == request.ProductId).Select(e => new UpdateProductResponse.Parameter()
            {
                IdParameter = e.Idparameter,
                Name = e.IdparameterNavigation.Name,
                MeasureUnit = e.IdparameterNavigation.Measureunit,
                Amount = e.Amount
            }).ToListAsync();
            var response = new UpdateProductResponse()
            {
                Name = product.Name,
                ProductId = product.Idproduct,
                Unit = product.Unit,
                Size = product.Size,
                HomeMeasure = product.Homemeasure,
                HomeMeasureSize = product.Homemeasuresize,
                Parameters = newparameters
            };
            return response;
        }
    }
}