using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using diet_server_api.DTO.Requests;
using diet_server_api.DTO.Requests.Patient;
using diet_server_api.DTO.Responses;
using diet_server_api.DTO.Responses.Doctor.Get;
using diet_server_api.DTO.Responses.Doctor.Search;
using diet_server_api.Exceptions;
using diet_server_api.Helpers;
using diet_server_api.Helpers.Calculators;
using diet_server_api.Models;
using diet_server_api.Services.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using static diet_server_api.DTO.Requests.SurveySignUpRequest;

namespace diet_server_api.Services.Implementation.Repository
{
    public class PatientService : IPatientService 
    {
        private readonly mdzcojxmContext _dbContext;

        public PatientService(mdzcojxmContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SurveyUserCreationResponse> CreatePatient(SurveySignUpRequest request)
        {

            var existingUser = await _dbContext.Users.AnyAsync(e => e.Email.ToLower() == request.Email.ToLower().Trim());
            if (existingUser) throw new AlreadyExists("Email already used");

            var existingPesel = await _dbContext.Users.AnyAsync(e => e.Pesel == request.PESEL);
            if (existingPesel) throw new AlreadyExists("PESEL already used");

            var existingPhoneNumber = await _dbContext.Users.AnyAsync(e => e.Phonenumber == request.PhoneNumber);
            if (existingPhoneNumber) throw new AlreadyExists("Phone number already used");

            if (request.DateOfBirth >= TimeConverter.GetCurrentPolishTime()) throw new InvalidData("Incorrect data of birth");

            var tempUser = await _dbContext.TempUsers.FirstOrDefaultAsync(e => e.Email.ToLower() == request.AccessEmail.ToLower().Trim());
            if (tempUser == null) throw new NotFound("User not found");

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
                Salt = salt,
                Isactive = true
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
                Date = TimeConverter.GetCurrentPolishTime(),
                Hipcircumference = request.HipCircumference,
                Waistcircumference = request.WaistCircumference,
                Whomeasured = Roles.PATIENT
            };

            await _dbContext.Measurements.AddAsync(measurements);

            var questionary = new Questionnaire()
            {
                IdpatientNavigation = patient,
                Databadania = TimeConverter.GetCurrentPolishTime(),
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
            await _dbContext.Questionnaires.AddAsync(questionary);

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

        public Task GetAllPatients()
        {
            throw new NotImplementedException();
        }

        public async Task<List<PatientSearchResponse>> GetPatientsByName(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                var patients = await _dbContext.Users.Where(e => e.Lastname.ToLower() == lastName.ToLower().Trim() && e.Role == Roles.PATIENT && e.Patient.Ispending == false && e.Isactive == true).Select(e => new PatientSearchResponse
                {
                    IdPatient = e.Iduser,
                    FirstName = e.Firstname,
                    LastName = e.Lastname,
                    DateOfBirth = e.Dateofbirth
                }).OrderBy(e => e.FirstName).ToListAsync();
                if (patients.Count == 0) throw new NotFound("No such patient found");
                return patients;
            }
            else
            {
                var patients = await _dbContext.Users.Where(e => e.Firstname.ToLower() == firstName.ToLower().Trim() && e.Lastname.ToLower() == lastName.ToLower().Trim() && e.Role == Roles.PATIENT && e.Patient.Ispending == false && e.Isactive == true).Select(e => new PatientSearchResponse
                {
                    IdPatient = e.Iduser,
                    FirstName = e.Firstname,
                    LastName = e.Lastname,
                    DateOfBirth = e.Dateofbirth
                }).OrderBy(e => e.FirstName).ToListAsync();
                if (patients.Count == 0) throw new NotFound("Patients not found");
                return patients;
            }
        }

        public async Task<PatientsByPageResponse> GetPatientPage(int page = 1)
        {
            if (page < 1) page = 1;
            int pageSize = 9;
            var rows = await _dbContext.Patients.Where(e => e.Ispending == false).CountAsync();
            var patients = await _dbContext.Users
            .Include(e => e.Patient)
            .Where(e => e.Patient.Ispending == false && e.Isactive == true)
            .OrderBy(e => e.Firstname)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(e => new PatientsByPageResponse.PatientByPage
            {
                IdPatient = e.Iduser,
                FirstName = e.Firstname,
                LastName = e.Lastname,
                DateOfBirth = e.Dateofbirth
            }).ToListAsync();
            return new PatientsByPageResponse()
            {
                Patients = patients,
                PageNumber = page,
                PageSize = pageSize,
                TotalRows = rows
            };
        }

        public async Task<GetPatientInfoResponse> GetPatientInfo(int idPatient)
        {
            var userExists = await _dbContext.Users.Include(e => e.Patient).AnyAsync(e => e.Iduser == idPatient && e.Role == Roles.PATIENT && e.Patient.Ispending == false && e.Isactive == true);
            if (!userExists) throw new NotFound("User not found");
            var patient = await _dbContext.Users.Include(e => e.Patient).Select(e => new
            {
                e.Iduser,
                e.Firstname,
                e.Lastname,
                e.Email,
                e.Pesel,
                Address = $"{e.Patient.Street}, {e.Patient.Flatnumber}, {e.Patient.City}",
                e.Phonenumber,
                e.Patient.Gender,
                Age = AgeCalculator.CalculateAge(e.Dateofbirth),
                CPM = e.Patient.Cpm,
                CorrectedValue = e.Patient.Correctedvalue,
                PAL = e.Patient.Pal
            }).FirstOrDefaultAsync(e => e.Iduser == idPatient);

            var measurments = await _dbContext.Measurements.OrderBy(e => e.Date).FirstOrDefaultAsync(e => e.Idpatient == idPatient);
            if (measurments == null) throw new NotFound("Measurments not found");

            var questionary = await _dbContext.Questionnaires.FirstOrDefaultAsync(e => e.Idpatient == idPatient);
            if (questionary == null) throw new NotFound("Questionary not found");

            var mealsExist = await _dbContext.Mealsbeforediets.AnyAsync(e => e.Idquestionary == questionary.Idquestionary);
            if (!mealsExist) throw new NotFound("Meal before diet not found");
            var meals = await _dbContext.Mealsbeforediets.Where(e => e.Idquestionary == questionary.Idquestionary).Select(e => new MealsBeforeDiet() { AtTime = e.Hour, MealNumber = e.Mealnumber, FoodToEat = e.Foodtoeat }).ToArrayAsync();


            //Calculators
            var idealBodyWeight = IdealBodyWeightCalculator.CalculateWeight(patient.Gender, measurments.Height);
            var modifiedFormula = ModifiedFormulaCalculator.CalculateFormula(measurments.Height);
            var basicMetabolism = BasicMetabolismCalculator.CalculateBasicMetabolism(patient.Gender, idealBodyWeight, measurments.Height, patient.Age);
            var waistHipRatio = decimal.Round(measurments.Waistcircumference / measurments.Hipcircumference, 2);

            var response = new GetPatientInfoResponse()
            {
                DateOfSurvey = questionary.Databadania,
                FirstName = patient.Firstname,
                LastName = patient.Lastname,
                Email = patient.Email,
                PESEL = patient.Pesel,
                Address = patient.Address,
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
                FoodBetweenMeals = questionary.Betweenmealsfood,
                CPM = patient.CPM,
                CorrectedValue = patient.CorrectedValue,
                PAL = patient.PAL
            };
            return response;
        }

        public async Task FillReport(FillDayReportRequest request, int idPatient)
        {
            var day = await _dbContext.Days.Include(e => e.DietiddietNavigation).FirstOrDefaultAsync(e => e.DietiddietNavigation.Idpatient == idPatient && e.Idday == request.IdDay);
            var days = await _dbContext.Days.Include(e => e.DietiddietNavigation).Select(e => e.DietiddietNavigation.Dailymeals).FirstOrDefaultAsync();
            if(day == null) throw new NotFound("Day not found");
            day.Patientreport = day.Patientreport == null ? request.PatientReport.Trim() : throw new AlreadyExists("Already filled");
            if(request.Meals.Count != days) throw new InvalidData("Not all mealtakes filled");
            foreach(var meal in request.Meals)
            {
                var mealTake = await _dbContext.Mealtakes.FirstOrDefaultAsync(e => e.Idmealtake == meal.IdMealTake);
                if(mealTake == null) throw new NotFound("Meal take not found");
                mealTake.Isfollowed = mealTake.Isfollowed == null ? meal.IsFollowed : throw new AlreadyExists("Already filled");
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}