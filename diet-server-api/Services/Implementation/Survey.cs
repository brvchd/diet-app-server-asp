using System;
using System.Linq;
using System.Threading.Tasks;
using diet_server_api.DTO.Requests;
using diet_server_api.DTO.Responses;
using diet_server_api.Exceptions;
using diet_server_api.Helpers;
using diet_server_api.Models;
using diet_server_api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace diet_server_api.Services.Implementation
{
    public class Survey : ISurvey
    {
        private readonly mdzcojxmContext _dbContext;
        public Survey(mdzcojxmContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<SurveyUserCreationResponse> CreateUserFromSurveyAsync(SurveySignUpRequest request)
        {
            var existingUser = await _dbContext.Users.AnyAsync(e => e.Email.Equals(request.Email));
            if (existingUser) throw new UserExistsExpection();
            var salt = HashPassword.GenerateSalt();
            var password = HashPassword.GeneratePassword(request.Password, salt);
            var user = new User(){
                Firstname = request.FirstName,
                Lastname = request.LastName,
                Dateofbirth = request.DateOfBirth,
                Email = request.Email,
                Password = password,
                Pesel = request.PESEL,
                Role = request.Role,
                Phonenumber = request.PhoneNumber,
                Salt = salt
            };
            _dbContext.Users.Add(user);
            
            //TO-DO token generation;

            var patient = new Patient(){
                IduserNavigation = user,
                Ispending = true,
                Gender = request.Gender,
                City = request.City,
                Street = request.Street,
                Flatnumber = request.FlatNumber,
                Streetnumber = request.StreetNumber,
            };
            _dbContext.Patients.Add(patient);

            var measurements = new Measurement(){
                IdpatientNavigation = patient,
                Height = request.Height,
                Weight = request.Weight,
                Date =  DateTime.UtcNow,
                Hipcircumference = request.HipCircumference,
                Waistcircumference = request.WaistCircumference,
                Whomeasured = request.FirstName + " " + request.LastName
            };

            return new SurveyUserCreationResponse();
        }

        public async Task<bool> ValidateSurveyCredentialsAsync(SurveyCheckCredentialsRequest request)
        {
            var existingUser = await _dbContext.TempUsers.AnyAsync(e => e.Email == request.Email && e.Uniquekey == request.UniqueKey);
            if (!existingUser) throw new UserDoesNotExistsException();
            return true;
        }
    }
}