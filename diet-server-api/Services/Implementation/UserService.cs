using System.Collections.Generic;
using System.Threading.Tasks;
using diet_server_api.Models;
using diet_server_api.Services.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace diet_server_api.Services.Implementation.Repository
{
    public class UserService : IUserService
    {
        private readonly mdzcojxmContext _dbContext;

        public UserService(mdzcojxmContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<User>> GetUsers()
        {
            return await _dbContext.Users.ToListAsync();
        }
    }
}