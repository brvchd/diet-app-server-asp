using System.Threading.Tasks;
using diet_server_api.DTO.Requests;
using diet_server_api.DTO.Responses.Survey;

namespace diet_server_api.Services.Interfaces
{
    public interface ISurveyService
    {
        Task<SurveyAccessResponse> ValidateSurveyCredentials(SurveyCheckCredentialsRequest request);

    }
}