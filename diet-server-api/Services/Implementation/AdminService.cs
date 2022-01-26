using System.Collections.Generic;
using System.Linq;
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

        public async Task ActiveAccount(int idUser)
        {
            var account = await _dbContext.Users.FirstOrDefaultAsync(e => e.Iduser == idUser && e.Isactive == false);
            if (account == null) throw new NotFound("Account was not found or already acitvated");
            account.Isactive = true;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<CreateUserResponse> CreateUser(CreateUserRequest request)
        {
            var exists = await _dbContext.Users.AnyAsync(e => e.Email == request.Email.ToLower().Trim());
            if (request.Role != Roles.DOCTOR.ToString() && request.Role != Roles.SECRETARY.ToString() && request.Role != Roles.ADMIN.ToString())
            {
                throw new InvalidData("Incorrect role provided");
            }
            if (exists) throw new AlreadyExists("Email already used!");
            var userAge = Helpers.Calculators.AgeCalculator.CalculateAge(request.DateOfBirth);
            if (userAge < 18) throw new InvalidData("User must be at least 18 years old");
            var peselExists = await _dbContext.Users.AnyAsync(e => e.Pesel == request.PESEL);
            if (peselExists) throw new AlreadyExists("Pesel already used");
            var phoneExists = await _dbContext.Users.AnyAsync(e => e.Phonenumber == request.PhoneNumber);
            if (phoneExists) throw new AlreadyExists("Phone number already used");
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

        public async Task DeactiveAccount(int idUser)
        {
            var account = await _dbContext.Users.FirstOrDefaultAsync(e => e.Iduser == idUser && e.Isactive == true);
            if (account == null) throw new NotFound("Account was not found or already deactivated");
            account.Isactive = false;
            account.Refreshtoken = null;
            account.Refreshtokenexp = null;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<GetUsersResponse> GetUsers(int page = 1)
        {
            if (page < 1) page = 1;
            int pageSize = 9;
            var rows = await _dbContext.Patients.Where(e => e.Ispending == false).CountAsync();
            var users = await _dbContext.Users
            .Include(e => e.Doctor)
            .Where(e => e.Role != "PATIENT")
            .Select(e => new GetUsersResponse.Employee
            {
                IdUser = e.Iduser,
                FirstName = e.Firstname,
                LastName = e.Lastname,
                DateOfBirth = e.Dateofbirth,
                Role = e.Role,
                PESEL = e.Pesel,
                Email = e.Email,
                Office = e.Role == "DOCTOR" ? e.Doctor.Office : null,
                IsActive = e.Isactive,
                PhoneNumber = e.Phonenumber
            })
            .OrderBy(e => e.FirstName)
            .Skip((page - 1) * pageSize)
            .Take(pageSize).ToListAsync();
            if (users.Count == 0) throw new NotFound("No matched results");
            var response = new GetUsersResponse()
            {
                Employees = users,
                PageNumber = page,
                PageSize = pageSize,
                TotalRows = rows
            };
            return response;
        }

        public async Task<List<SearchUsersResponse>> SearchUser(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                var users = await _dbContext.Users
                .Include(e => e.Doctor)
                .Where(e => e.Lastname.ToLower() == lastName.ToLower().Trim() && e.Role != Roles.PATIENT)
                .Select(e => new SearchUsersResponse
                {
                    IdUser = e.Iduser,
                    FirstName = e.Firstname,
                    LastName = e.Lastname,
                    DateOfBirth = e.Dateofbirth,
                    Role = e.Role,
                    PESEL = e.Pesel,
                    Email = e.Email,
                    Office = e.Role == "DOCTOR" ? e.Doctor.Office : null,
                    IsActive = e.Isactive,
                    PhoneNumber = e.Phonenumber

                }).OrderBy(e => e.FirstName).ToListAsync();
                if (users.Count == 0) throw new NotFound("No matched results");
                return users;
            }
            else
            {
                var users = await _dbContext.Users
                .Where(e => e.Firstname.ToLower() == firstName.ToLower().Trim() && e.Lastname.ToLower() == lastName.ToLower().Trim() && e.Role != Roles.PATIENT)
                .Include(e => e.Doctor)
                .Select(e => new SearchUsersResponse
                {
                    IdUser = e.Iduser,
                    FirstName = e.Firstname,
                    LastName = e.Lastname,
                    DateOfBirth = e.Dateofbirth,
                    Role = e.Role,
                    PESEL = e.Pesel,
                    Email = e.Email,
                    Office = e.Role == "DOCTOR" ? e.Doctor.Office : null,
                    IsActive = e.Isactive,
                    PhoneNumber = e.Phonenumber
                }).OrderBy(e => e.FirstName).ToListAsync();
                if (users.Count == 0) throw new NotFound("Patients not found");
                return users;
            }
        }
    }
}