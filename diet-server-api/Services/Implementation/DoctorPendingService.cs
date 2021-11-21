using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using diet_server_api.DTO.Requests.Doctor;
using diet_server_api.DTO.Responses.Doctor;
using diet_server_api.Exceptions;
using diet_server_api.Helpers;
using diet_server_api.Helpers.Calculators;
using diet_server_api.Models;
using diet_server_api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using static diet_server_api.DTO.Requests.SurveySignUpRequest;

namespace diet_server_api.Services.Implementation
{
    public class DoctorPendingService : IDoctorPendingService
    {
        private readonly mdzcojxmContext _dbContext;

        public DoctorPendingService(mdzcojxmContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<PendingPatientResponse>> GetPatients()
        {
            var users = await _dbContext.Users.Join(_dbContext.Patients, user => user.Iduser, patient => patient.Iduser, (user, patient) => new
            {
                IdPatient = patient.Iduser,
                FirstName = user.Firstname,
                LastName = user.Lastname,
                IsPending = patient.Ispending
            }).Where(e => e.IsPending == true).Select(e => new PendingPatientResponse() { IdUser = e.IdPatient, FirstName = e.FirstName, LastName = e.LastName }).OrderByDescending(e => e.IdUser).ToListAsync();

            return users;
        }


        public async Task<PendingPatientDataResponse> GetPatientData(int idpatient)
        {
            var userExists = await _dbContext.Users.AnyAsync(e => e.Iduser == idpatient && e.Role == Roles.PATIENT);
            if (!userExists) throw new UserNotFound();
            var patient = await _dbContext.Users.Join(_dbContext.Patients, user => user.Iduser, patient => patient.Iduser, (user, patient) => new
            {
                user.Iduser,
                user.Firstname,
                user.Lastname,
                user.Email,
                user.Pesel,
                Adress = patient.City + " " + patient.Street + " " + patient.Flatnumber,
                user.Phonenumber,
                patient.Gender,
                Age = AgeCalculator.CalculateAge(user.Dateofbirth)
            }).FirstOrDefaultAsync(e => e.Iduser == idpatient);

            var measurments = await _dbContext.Measurements.OrderBy(e => e.Date).FirstOrDefaultAsync(e => e.Idpatient == idpatient);
            if (measurments == null) throw new MeasurmentsNotFound();

            var questionary = await _dbContext.Questionaries.FirstOrDefaultAsync(e => e.Idpatient == idpatient);
            if (questionary == null) throw new QuestionaryNotFound();

            var mealsExist = await _dbContext.Mealsbeforediets.AnyAsync(e => e.Idquestionary == questionary.Idquestionary);
            if (!mealsExist) throw new MealsBeforeDietNotFound();
            var meals = await _dbContext.Mealsbeforediets.Where(e => e.Idquestionary == questionary.Idquestionary).Select(e => new MealsBeforeDiet() { AtTime = e.Hour, MealNumber = e.Mealnumber, FoodToEat = e.Foodtoeat }).ToArrayAsync();


            //Calculators
            var idealBodyWeight = IdealBodyWeightCalculator.CalculateWeight(patient.Gender, measurments.Height);
            var modifiedFormula = ModifiedFormulaCalculator.CalculateFormula(measurments.Height);
            var basicMetabolism = BasicMetabolismCalculator.CalculateBasicMetabolism(patient.Gender, idealBodyWeight, measurments.Height, patient.Age);
            var waistHipRatio = decimal.Round(measurments.Waistcircumference / measurments.Hipcircumference, 2);

            var response = new PendingPatientDataResponse()
            {
                DateOfSurvey = questionary.Databadania,
                FirstName = patient.Firstname,
                LastName = patient.Lastname,
                Email = patient.Email,
                PESEL = patient.Pesel,
                Address = patient.Adress,
                PhoneNumber = patient.Phonenumber,
                Gender = patient.Gender,
                Age = patient.Age,
                Weight = measurments.Weight,
                Height = measurments.Height,
                IdealBodyWeight = idealBodyWeight,
                ModifiedFormula = modifiedFormula,
                BasicMetabolism = basicMetabolism,
                WaistCircumference = measurments.Waistcircumference,
                HipCircumference = measurments.Hipcircumference,
                WaistHipRatio = waistHipRatio,
                ConsultationGoal = questionary.Mainproblems,
                Diabetes = questionary.Diabetes,
                Hypertension = questionary.Hypertension,
                InsulinResistance = questionary.Insulinresistance,
                Hypothyroidism = questionary.Hypertension,
                IntestinalDiseases = questionary.Intestinaldiseases,
                OtherDiseases = questionary.Otherdiseases,
                Medications = questionary.Medications,
                DietSupplements = questionary.Supplementstaken,
                GetUpInterval = questionary.Usuallywakeup,
                GoToSleepInterval = questionary.Usuallygotosleep,
                AvgSleep = questionary.Avgsleep,
                SportsPerDay = questionary.Excercisingperday,
                SportsPerWeek = questionary.Exercisingperweek,
                RegularWalk = questionary.Regularwalk,
                SportTypes = questionary.Sporttypes,
                WaterGlasses = questionary.Waterglasses,
                CoffeeGlasses = questionary.Waterglasses,
                TeaGlasses = questionary.Teaglasses,
                JuiceGlasses = questionary.Juiceglasses,
                EnergyDrinkGlasses = questionary.Energydrinkglasses,
                Alcohol = questionary.Alcoholinfo,
                Cigs = questionary.Cigs,
                Breakfast = questionary.Breakfast,
                SecondBreakfast = questionary.Secondbreakfast,
                Lunch = questionary.Lunch,
                AfternoonMeal = questionary.Afternoonmeal,
                Dinner = questionary.Dinner,
                FavFood = questionary.Favfooditems,
                NotFavFood = questionary.Notfavfooditems,
                HypersensProds = questionary.Hypersensitivityproducts,
                AlergieProds = questionary.Alergieproducts,
                Meals = meals,
                FoodBetweenMeals = questionary.Betweenmealsfood
            };
            return response;
        }

        public async Task AcceptPatient(PendingPatientAccept request)
        {
            var user = await _dbContext.Patients.FirstOrDefaultAsync(e => e.Iduser == request.IdPatient);
            if (user == null) throw new UserNotFound("Patient not found");
            user.Correctedvalue = request.CorrectedValue;
            user.Cpm = request.CPM;
            user.Ispending = false;
            await _dbContext.SaveChangesAsync();
        }

        public async Task RejectPendingPatient(int idPatient)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(e => e.Iduser == idPatient && e.Role == Roles.PATIENT);
            if (user == null) throw new UserNotFound();
            var patient = await _dbContext.Patients.FirstOrDefaultAsync(e => e.Iduser == idPatient);
            var questionary = await _dbContext.Questionaries.FirstOrDefaultAsync(e => e.Idpatient == idPatient);
            var measurements = await _dbContext.Measurements.FirstOrDefaultAsync(e => e.Idpatient == idPatient);
            var mealsList = await _dbContext.Mealsbeforediets.Where(e => e.Idquestionary == questionary.Idquestionary).ToListAsync();
            foreach (var meal in mealsList)
            {
                _dbContext.Mealsbeforediets.Remove(meal);
            }
            _dbContext.Questionaries.Remove(questionary);
            _dbContext.Measurements.Remove(measurements);
            _dbContext.Patients.Remove(patient);
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}