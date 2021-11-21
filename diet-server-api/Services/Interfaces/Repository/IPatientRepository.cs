using System.Threading.Tasks;
using diet_server_api.DTO.Requests;
using diet_server_api.DTO.Responses;
using diet_server_api.DTO.Responses.Doctor;

namespace diet_server_api.Services.Interfaces.Repository
{
    public interface IPatientRepository
    {
        public Task<SurveyUserCreationResponse> CreatePatient(SurveySignUpRequest request);
        public Task<GetPatientsByPageResponse> GetPatientsByPage(int page);
        public Task GetAllPatients();

    }
}