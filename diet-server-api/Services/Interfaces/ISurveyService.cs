using System.Threading.Tasks;
using diet_server_api.DTO.Requests;
using diet_server_api.DTO.Responses;

namespace diet_server_api.Services.Interfaces
{
    public interface ISurveyService
    {
        public Task<SurveyUserCreationResponse> ValidateSurveyCredentials(SurveyCheckCredentialsRequest request);
        public Task<SurveyUserCreationResponse> CreateUserFromSurvey(SurveySignUpRequest request);
        public Task DeleteTempUser(string email);

    }
}