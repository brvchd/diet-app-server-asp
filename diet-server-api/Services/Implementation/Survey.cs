using System;
using System.Threading.Tasks;
using diet_server_api.DTO.Requests;
using diet_server_api.DTO.Responses;
using diet_server_api.Exceptions;
using diet_server_api.Helpers;
using diet_server_api.Models;
using diet_server_api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace diet_server_api.Services.Implementation
{
    public class Survey : ISurvey
    {        private readonly mdzcojxmContext _dbContext;
        private const string PATIEN_ROLE = "PATIENT";
        public Survey(mdzcojxmContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<SurveyUserCreationResponse> CreateUserFromSurveyAsync(SurveySignUpRequest request)
        {
            var existingUser = await _dbContext.Users.AnyAsync(e => e.Email.Equals(request.Email));
            if (existingUser) throw new UserExistsExpection();
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
                Role = PATIEN_ROLE,
                Phonenumber = request.PhoneNumber,
                Salt = salt
            };
            _dbContext.Users.Add(user);

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
            _dbContext.Patients.Add(patient);

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

            _dbContext.Measurements.Add(measurements);

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
            _dbContext.Questionaries.Add(questionary);

            foreach (var mealTake in request.Meals)
            {
                var meal = new Mealsbeforediet()
                {
                    IdquestionaryNavigation = questionary,
                    Mealnumber = mealTake.MealNumber,
                    Hour = mealTake.AtTime,
                    Foodtoeat = mealTake.FoodToEat
                };
                _dbContext.Mealsbeforediets.Add(meal);
            }

            await _dbContext.SaveChangesAsync();
            return new SurveyUserCreationResponse() { Message = "Created" };
        }

        public async Task<bool> ValidateSurveyCredentialsAsync(SurveyCheckCredentialsRequest request)
        {
            var existingUser = await _dbContext.TempUsers.AnyAsync(e => e.Email == request.Email && e.Uniquekey == request.UniqueKey);
            if (!existingUser) throw new UserDoesNotExistsException();
            return true;
        }
    }
}