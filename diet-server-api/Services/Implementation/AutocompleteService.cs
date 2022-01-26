using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using diet_server_api.DTO.Responses.Autocomplete;
using diet_server_api.DTO.Responses.KnowledgeBase.Search;
using diet_server_api.DTO.Responses.Secretary;
using diet_server_api.Exceptions;
using diet_server_api.Helpers;
using diet_server_api.Models;
using diet_server_api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace diet_server_api.Services.Implementation
{
    public class AutocompleteService : IAutocompleteService  
    {
        private readonly mdzcojxmContext _dbContext;

        public AutocompleteService(mdzcojxmContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<AutocompleteDiseases>> AutocompleteDiseases()
        {
            var diseases = await _dbContext.Diseases.Select(e => new AutocompleteDiseases
            {
                DiseaseName = e.Name
            }).ToListAsync();
            if(diseases.Count == 0) throw new NotFound("No diseases");
            return diseases;
        }

        public async Task<List<AutocompleteUsers>> AutocompleteDoctors()
        {
            var doctors = await _dbContext.Users
            .Where(e => e.Role == Roles.DOCTOR.ToString() && e.Isactive == true)
            .Select(e => new AutocompleteUsers
            {
                FirstName = e.Firstname,
                LastName = e.Lastname
            }).ToListAsync();
            if(doctors.Count == 0) throw new NotFound("No doctors found");
            return doctors;
        }

        public async Task<List<AutocompleteMeal>> AutocompleteMeals()
        {
            var meals = await _dbContext.Meals.Select(e => new AutocompleteMeal
            {
                MealName = e.Nameofmeal
            }).ToListAsync();
            if(meals.Count == 0) throw new NotFound("No products found");
            return meals;
        }

        public async Task<List<AutocompleteUsers>> AutocompletePatients()
        {
            var patients = await _dbContext.Users
            .Include(e => e.Patient)
            .Where(e => e.Role == Roles.PATIENT.ToString() && e.Isactive == true && e.Patient.Ispending == false)
            .Select(e => new AutocompleteUsers
            {
                FirstName = e.Firstname,
                LastName = e.Lastname
            }).ToListAsync();
            if(patients.Count == 0) throw new NotFound("No doctors found");
            return patients;
        }

        public async Task<List<AutocompleteProduct>> AutocompleteProducts()
        {
            var products = await _dbContext.Products.Select(e => new AutocompleteProduct
            {
                ProductName = e.Name
            }).ToListAsync();
            if(products.Count == 0) throw new NotFound("No products found");
            return products;
        }

        public async Task<List<AutocompleteSupplements>> AutocompleteSupplements()
        {
            var supplements = await _dbContext.Supplements.Select(e => new AutocompleteSupplements
            {
                SupplementName = e.Name
            }).ToListAsync();
            if(supplements.Count == 0) throw new NotFound("Supplements not found");
            return supplements;
        }

        public async Task<List<AutocompleteUsers>> AutocompleteUsers()
        {
            var users = await _dbContext.Users.Where(e => e.Isactive == true && e.Role != Roles.PATIENT.ToString()).Select(e => new AutocompleteUsers
            {
                FirstName = e.Firstname,
                LastName = e.Lastname
            }).ToListAsync();
            if(users.Count == 0) throw new NotFound("Users not found");
            return users;
        }
    }
}