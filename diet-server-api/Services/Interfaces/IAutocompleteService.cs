using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using diet_server_api.DTO.Responses.Autocomplete;
using diet_server_api.DTO.Responses.KnowledgeBase.Search;
using diet_server_api.DTO.Responses.Secretary;

namespace diet_server_api.Services.Interfaces
{
    public interface IAutocompleteService
    {
        Task<List<AutocompleteUsers>> AutocompleteUsers();
        Task<List<AutocompleteUsers>> AutocompletePatients();
        Task<List<AutocompleteUsers>> AutocompleteDoctors();
        Task<List<AutocompleteDiseases>> AutocompleteDiseases();
        Task<List<AutocompleteSupplements>> AutocompleteSupplements();
        Task<List<AutocompleteMeal>> AutocompleteMeals();
        Task<List<AutocompleteProduct>> AutocompleteProducts();
    }
}