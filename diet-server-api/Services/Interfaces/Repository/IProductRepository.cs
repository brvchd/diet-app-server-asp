using System.Collections.Generic;
using System.Threading.Tasks;
using diet_server_api.DTO.Requests.KnowledgeBase.Add;
using diet_server_api.DTO.Requests.KnowledgeBase.Update;
using diet_server_api.DTO.Responses.Doctor.Update;
using diet_server_api.DTO.Responses.KnowledgeBase.Add;
using diet_server_api.DTO.Responses.KnowledgeBase.Get;

namespace diet_server_api.Services.Interfaces.Repository
{
    public interface IProductRepository
    {

        Task<AddProductResponse> AddProduct(AddProductRequest request);
        Task<GetProductsResponse> GetProducts(int page);
        Task<List<GetProductsResponse.Product>> SearchProduct(string product);
        Task<UpdateProductResponse> UpdateProduct(UpdateProductRequest request);
        Task<List<GetProductParametersResponse>> GetProductParams(int IdProduct);



    }
}