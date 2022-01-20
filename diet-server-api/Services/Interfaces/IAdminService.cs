using System.Collections.Generic;
using System.Threading.Tasks;
using diet_server_api.DTO.Requests.Admin;
using diet_server_api.DTO.Responses.Admin;

namespace diet_server_api.Services.Interfaces.Repository
{
    public interface IAdminService
    {
        Task<CreateUserResponse> CreateUser(CreateUserRequest request);
        Task DeactiveAccount(int idUser);
        Task ActiveAccount(int idUser);
        Task<GetUsersResponse> GetUsers(int page);
        Task<List<SearchUsersResponse>> SearchUser(string firstName, string lastName);
    }
}