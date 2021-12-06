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
    public class DoctorRepository : IDoctorRepository
    {
        private readonly mdzcojxmContext _dbContext;

        public DoctorRepository(mdzcojxmContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DoctorCreatorResponse> CreateDoctor(DoctorCreatorRequest request)
        { 
            var exists = await _dbContext.Users.AnyAsync(e => e.Email == request.Email);
            if (exists) throw new AlreadyExists("Doctor already exists!");
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
                Role = Roles.DOCTOR,
                Salt = salt
            };
            await _dbContext.Users.AddAsync(user);
            var doctor = new Doctor()
            {
                IduserNavigation = user,
                Office = request.Office
            };
            await _dbContext.Doctors.AddAsync(doctor);
            await _dbContext.SaveChangesAsync();
            return new DoctorCreatorResponse()
            {
                Email = user.Email
            };
        }
    }
}