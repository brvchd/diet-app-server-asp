using System.Collections.Generic;
using System.Threading.Tasks;
using diet_server_api.Models;

namespace diet_server_api.Services.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsers();
    }
}