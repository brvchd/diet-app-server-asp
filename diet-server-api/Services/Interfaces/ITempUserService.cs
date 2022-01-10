using System.Collections.Generic;
using System.Threading.Tasks;
using diet_server_api.DTO.Requests.Admin;
using diet_server_api.Models;

namespace diet_server_api.Services.Interfaces.Repository
{
    public interface ITempUserService
    {
        Task<TempUser> AddTempUser(TemporaryUserRequest user);
        Task<List<TempUser>> GetTempUsers();
        Task DeleteTempUser(string email);
    }
}