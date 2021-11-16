using System.Collections.Generic;
using System.Threading.Tasks;
using diet_server_api.DTO.Requests.Admin;
using diet_server_api.DTO.Responses.Admin;
using diet_server_api.Models;

namespace diet_server_api.Services.Interfaces.Repository
{
    public interface ITempUserRepositoryService
    {
        public Task<TempUser> AddTempUser(TemporaryUserRequest user);
        public Task<List<TempUser>> GetTempUsers();
        public Task DeleteTempUser(string email);
    }
}