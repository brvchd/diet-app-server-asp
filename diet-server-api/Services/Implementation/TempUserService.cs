using System.Collections.Generic;
using System.Threading.Tasks;
using diet_server_api.DTO.Requests.Admin;
using diet_server_api.Exceptions;
using diet_server_api.Models;
using diet_server_api.Services.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace diet_server_api.Services.Implementation.Repository
{
    public class TempUserService : ITempUserService
    {
        private readonly mdzcojxmContext _dbContext;

        public TempUserService(mdzcojxmContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TempUser> AddTempUser(TemporaryUserRequest user)
        {
            var exists = await _dbContext.TempUsers.AnyAsync(e => e.Email.ToLower().Trim() == user.Email.ToLower().Trim());
            if (exists) throw new AlreadyExists("Temp user already exists");
            var tempUser = new TempUser()
            {
                Email = user.Email.Trim(),
                Uniquekey = user.UniqueKey
            };
            await _dbContext.TempUsers.AddAsync(tempUser);
            await _dbContext.SaveChangesAsync();
            return tempUser;
        }

        public Task DeleteTempUser(string email)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<TempUser>> GetTempUsers()
        {
            return await _dbContext.TempUsers.ToListAsync();
        }
    }
}