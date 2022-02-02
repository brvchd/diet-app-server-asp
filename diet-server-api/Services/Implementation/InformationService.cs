using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using diet_server_api.DTO.Responses.Diet;
using diet_server_api.Exceptions;
using diet_server_api.Helpers.Calculators;
using diet_server_api.Models;
using diet_server_api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace diet_server_api.Services.Implementation
{
    public class InformationService : IInformationService   
    {
        private readonly mdzcojxmContext _dbContext;

        public InformationService(mdzcojxmContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetFullInfoResponse> GetFullPatientInfo(int idPatient, int? idDiet)
        {
            var userExists = await _dbContext.Users.AnyAsync(e => e.Iduser == idPatient && e.Isactive == true && e.Patient.Ispending == false);
            if(!userExists) throw new NotFound("Patient not found");
            var diet = await _dbContext.Diets.Where(e => e.Idpatient == idPatient).OrderByDescending(e => e.Iddiet).Select(e => e.Name).FirstOrDefaultAsync();
            string currentDiet = "";
            string selectedDiet = "";
            if(diet != null) currentDiet = diet;
            if(idDiet != null)
            {
                var patientsDiet = await _dbContext.Diets.AnyAsync(e => e.Iddiet == idDiet && e.Idpatient == idPatient);
                if(!patientsDiet) throw new NotFound("No diet found"); 
                selectedDiet = await _dbContext.Diets.Where(e => e.Iddiet == idDiet && e.Idpatient == idPatient).Select(e => e.Name).FirstOrDefaultAsync();
            }
            var user = await _dbContext.Users
            .Include(e => e.Patient)
            .ThenInclude(e => e.Questionnaires)
            .Include(e => e.Patient)
            .ThenInclude(e => e.Diets)
            .Where(e => e.Iduser == idPatient)
            .Select(e => new GetFullInfoResponse
            {
                FirstName = e.Firstname,
                LastName = e.Lastname,
                Age = AgeCalculator.CalculateAge(e.Dateofbirth),
                Email = e.Email,
                PESEL = e.Pesel,
                PhoneNumber = e.Phonenumber,
                MainProblems = e.Patient.Questionnaires.First().Mainproblems,
                Diabetes = e.Patient.Questionnaires.First().Diabetes,
                Hypertension = e.Patient.Questionnaires.First().Hypertension,
                InsulinResistance = e.Patient.Questionnaires.First().Insulinresistance,
                IntestinalDiseases = e.Patient.Questionnaires.First().Intestinaldiseases,
                OtherDiseases = e.Patient.Questionnaires.First().Otherdiseases,
                FavFood = e.Patient.Questionnaires.First().Favfooditems,
                NotFavFood = e.Patient.Questionnaires.First().Notfavfooditems,
                HypersensProds = e.Patient.Questionnaires.First().Hypersensitivityproducts,
                AlergieProds = e.Patient.Questionnaires.First().Alergieproducts,
                PAL = e.Patient.Pal,
                CurrentDiet = currentDiet,
                SelectedDiet = selectedDiet  
            })
            .FirstOrDefaultAsync();
            return user;
        }

        public async Task<GetLessInfoResponse> GetLessInfoResponse(int idPatient, int? idDiet)
        {
            var userExists = await _dbContext.Users.AnyAsync(e => e.Iduser == idPatient && e.Isactive == true && e.Patient.Ispending == false);
            if(!userExists) throw new NotFound("Patient not found");
            var diet = await _dbContext.Diets.Where(e => e.Idpatient == idPatient).OrderByDescending(e => e.Iddiet).Select(e => e.Name).FirstOrDefaultAsync();
            string currentDiet = "";
            string selectedDiet = "";
            if(diet != null) currentDiet = diet;
            if(idDiet != null)
            {
                var patientsDiet = await _dbContext.Diets.AnyAsync(e => e.Iddiet == idDiet && e.Idpatient == idPatient);
                if(!patientsDiet) throw new NotFound("No diet found"); 
                selectedDiet = await _dbContext.Diets.Where(e => e.Iddiet == idDiet && e.Idpatient == idPatient).Select(e => e.Name).FirstOrDefaultAsync();
            }
            var user = await _dbContext.Users
            .Include(e => e.Patient)
            .ThenInclude(e => e.Diets)
            .Where(e => e.Iduser == idPatient)
            .Select(e => new GetLessInfoResponse
            {
                FirstName = e.Firstname,
                LastName = e.Lastname,
                Age = AgeCalculator.CalculateAge(e.Dateofbirth),
                PESEL = e.Pesel,
                CurrentDietName = currentDiet,
                SelectedDiet = selectedDiet
            })
            .FirstOrDefaultAsync();
            return user;
        }

        public async Task<GetMidInfoResponse> GetMidPatientInfo(int idPatient)
        {
            var userExists = await _dbContext.Users.AnyAsync(e => e.Iduser == idPatient && e.Isactive == true && e.Patient.Ispending == false);
            if(!userExists) throw new NotFound("Patient not found");
            var diet = await _dbContext.Diets.Where(e => e.Idpatient == idPatient).OrderByDescending(e => e.Iddiet).Select(e => e.Name).FirstOrDefaultAsync();
            string currentDiet = "";
            if(diet != null) currentDiet = diet;
            
            var user = await _dbContext.Users
            .Include(e => e.Patient)
            .ThenInclude(e => e.Questionnaires)
            .Include(e => e.Patient)
            .ThenInclude(e => e.Diets)
            .Where(e => e.Iduser == idPatient)
            .Select(e => new GetMidInfoResponse    
            {
                FirstName = e.Firstname,
                LastName = e.Lastname,
                Age = AgeCalculator.CalculateAge(e.Dateofbirth),
                PESEL = e.Pesel,
                Diabetes = e.Patient.Questionnaires.First().Diabetes,
                Hypertension = e.Patient.Questionnaires.First().Hypertension,
                InsulinResistance = e.Patient.Questionnaires.First().Insulinresistance,
                IntestinalDiseases = e.Patient.Questionnaires.First().Intestinaldiseases,
                OtherDiseases = e.Patient.Questionnaires.First().Otherdiseases,
                CurrentDiet = currentDiet   
            })
            .FirstOrDefaultAsync();
            return user;
        }
    }
}