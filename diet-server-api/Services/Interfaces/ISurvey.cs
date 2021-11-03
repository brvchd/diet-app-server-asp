using System;
using System.Threading.Tasks;
using diet_server_api.DTO.Requests;
using diet_server_api.DTO.Responses;
using diet_server_api.Models;

namespace diet_server_api.Services.Interfaces
{
    public interface ISurvey
    {
        public Task<bool> ValidateSurveyCredentialsAsync(SurveyCheckCredentialsRequest request);
        public Task<SurveyUserCreationResponse> CreateUserFromSurveyAsync(SurveySignUpRequest request);

    }
}