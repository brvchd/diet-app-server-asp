using System;
using System.Threading.Tasks;
using diet_server_api.DTO.Requests;
using diet_server_api.DTO.Responses;
using diet_server_api.Exceptions;
using diet_server_api.Helpers;
using diet_server_api.Models;
using diet_server_api.Services.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace diet_server_api.Services.Implementation.Repository
{
    public class PatientRepositoryService : IPatientRepositoryService
    {
        private readonly mdzcojxmContext _dbContext;

        public PatientRepositoryService(mdzcojxmContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SurveyUserCreationResponse> CreatePatient(SurveySignUpRequest request)
        {

            var existingUser = await _dbContext.Users.AnyAsync(e => e.Email == request.Email);
            if (existingUser) throw new UserAlreadyExists();

            var existingPesel = await _dbContext.Users.AnyAsync(e => e.Pesel == request.PESEL);
            if (existingPesel) throw new UserAlreadyExists("PESEL already exists");

            var existingPhoneNumber = await _dbContext.Users.AnyAsync(e => e.Phonenumber == request.PhoneNumber);
            if (existingPhoneNumber) throw new UserAlreadyExists("Phone number already exists");

            var tempUser = await _dbContext.TempUsers.FirstOrDefaultAsync(e => e.Email == request.AccessEmail);
            if (tempUser == null) throw new UserNotFound("No such user");

            _dbContext.TempUsers.Remove(tempUser);

            var salt = SaltGenerator.GenerateSalt();
            var password = PasswordGenerator.GeneratePassword(request.Password, salt);
            var user = new User()
            {
                Firstname = request.FirstName,
                Lastname = request.LastName,
                Dateofbirth = request.DateOfBirth,
                Email = request.Email,
                Password = password,
                Pesel = request.PESEL,
                Role = Roles.PATIENT,
                Phonenumber = request.PhoneNumber,
                Salt = salt
            };
            await _dbContext.Users.AddAsync(user);

            var patient = new Patient()
            {
                IduserNavigation = user,
                Ispending = true,
                Gender = request.Gender,
                City = request.City,
                Street = request.Street,
                Flatnumber = request.FlatNumber,
                Streetnumber = request.StreetNumber,
            };
            await _dbContext.Patients.AddAsync(patient);

            var measurements = new Measurement()
            {
                IdpatientNavigation = patient,
                Height = request.Height,
                Weight = request.Weight,
                Date = DateTime.UtcNow,
                Hipcircumference = request.HipCircumference,
                Waistcircumference = request.WaistCircumference,
                Whomeasured = request.FirstName + " " + request.LastName
            };

            await _dbContext.Measurements.AddAsync(measurements);

            var questionary = new Questionary()
            {
                IdpatientNavigation = patient,
                Databadania = DateTime.UtcNow,
                Education = request.Education,
                Profession = request.Profession,
                Mainproblems = request.MainProblems,
                Hypertension = request.Hypertension,
                Insulinresistance = request.InsulinResistance,
                Diabetes = request.Diabetes,
                Hypothyroidism = request.Hypothyroidism,
                Intestinaldiseases = request.IntestinalDiseases,
                Otherdiseases = request.OtherDiseases,
                Medications = request.Medications,
                Supplementstaken = request.SupplementsTaken,
                Avgsleep = request.AvgSleep,
                Usuallywakeup = request.UsuallyWakeUp,
                Usuallygotosleep = request.UsuallyGoToSleep,
                Regularwalk = request.RegularWalk,
                Excercisingperday = request.ExercisingPerDay,
                Sporttypes = request.SportTypes,
                Exercisingperweek = request.ExercisingPerWeek,
                Waterglasses = request.WaterGlasses,
                Coffeeglasses = request.CoffeeGlasses,
                Teaglasses = request.TeaGlasses,
                Energydrinkglasses = request.EnergyDrinkGlasses,
                Juiceglasses = request.JuiceGlasses,
                Alcoholinfo = request.AlcoholInfo,
                Cigs = request.Cigs,
                Breakfast = request.Breakfast,
                Secondbreakfast = request.SecondBreakfast,
                Lunch = request.Lunch,
                Afternoonmeal = request.AfternoonMeal,
                Dinner = request.Dinner,
                Favfooditems = request.FavouriteFoodItems,
                Notfavfooditems = request.NotFavouriteFoodItems,
                Hypersensitivityproducts = request.HypersensitivityProducts,
                Alergieproducts = request.AlergieProducts,
                Betweenmealsfood = request.FoodBetweenMeals
            };
            await _dbContext.Questionaries.AddAsync(questionary);

            foreach (var mealTake in request.Meals)
            {
                var meal = new Mealsbeforediet()
                {
                    IdquestionaryNavigation = questionary,
                    Mealnumber = mealTake.MealNumber,
                    Hour = mealTake.AtTime,
                    Foodtoeat = mealTake.FoodToEat
                };
                await _dbContext.Mealsbeforediets.AddAsync(meal);
            }
            await _dbContext.SaveChangesAsync();
            return new SurveyUserCreationResponse() { Message = "Created" };
        }
    }
}