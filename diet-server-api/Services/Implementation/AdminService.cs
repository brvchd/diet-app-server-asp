using System.Threading.Tasks;
using diet_server_api.DTO.Requests.Admin;
using diet_server_api.DTO.Responses.Admin;
using diet_server_api.Exceptions;
using diet_server_api.Helpers;
using diet_server_api.Models;
using diet_server_api.Services.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace diet_server_api.Services.Implementation.Repository
{
    public class AdminService : IAdminService
    {
        private readonly mdzcojxmContext _dbContext;

        public AdminService(mdzcojxmContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CreateUserResponse> CreateUser(CreateUserRequest request)
        {
            var exists = await _dbContext.Users.AnyAsync(e => e.Email == request.Email);
            if (request.Role != Roles.DOCTOR.ToString() && request.Role != Roles.SECRETARY.ToString() && request.Role != Roles.ADMIN.ToString())
            {
                throw new InvalidData("Incorrect role provided");
            }
            if (exists) throw new AlreadyExists("User already already exists!");
            var userAge = Helpers.Calculators.AgeCalculator.CalculateAge(request.DateOfBirth);
            if (userAge < 18) throw new InvalidData("User must be at least 18 years old");
            var salt = SaltGenerator.GenerateSalt();
            var password = PasswordGenerator.GeneratePassword(request.Password, salt);
            var user = new User()
            {
                Firstname = request.FirstName,
                Lastname = request.LastName,
                Email = request.Email,
                Pesel = request.PESEL,
                Phonenumber = request.PhoneNumber,
                Password = password,
                Dateofbirth = request.DateOfBirth,
                Role = request.Role,
                Salt = salt,
                Isactive = true
            };
            await _dbContext.Users.AddAsync(user);
            if (request.Role == "DOCTOR")
            {
                if (request.Office == null) throw new InvalidData("Office number must be provided");
                var doctor = new Doctor()
                {
                    IduserNavigation = user,
                    Office = request.Office
                };
                await _dbContext.Doctors.AddAsync(doctor);
            }
            await _dbContext.SaveChangesAsync();
            return new CreateUserResponse()
            {
                Email = user.Email,
                Password = request.Password
            };
        }
    }
}