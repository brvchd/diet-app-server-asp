using System.Threading.Tasks;
using diet_server_api.DTO.Requests;
using diet_server_api.DTO.Responses;
using diet_server_api.DTO.Responses.Survey;

namespace diet_server_api.Services.Interfaces
{
    public interface ISurveyService
    {
        public Task<SurveyAccessResponse> ValidateSurveyCredentials(SurveyCheckCredentialsRequest request);
        public Task<SurveyUserCreationResponse> CreateUserFromSurvey(SurveySignUpRequest request);
    }
}