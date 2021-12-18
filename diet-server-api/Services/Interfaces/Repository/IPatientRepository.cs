using System.Collections.Generic;
using System.Threading.Tasks;
using diet_server_api.DTO.Requests;
using diet_server_api.DTO.Responses;
using diet_server_api.DTO.Responses.Doctor.Get;
using diet_server_api.DTO.Responses.Doctor.Search;

namespace diet_server_api.Services.Interfaces.Repository
{
    public interface IPatientRepository
    {
        Task<SurveyUserCreationResponse> CreatePatient(SurveySignUpRequest request);
        Task<PatientsByPageResponse> GetPatientPage(int page);
        Task<List<PatientSearchResponse>> GetPatientsByName(string firstName, string lastName);
        Task GetAllPatients();
        Task<GetPatientInfoResponse> GetPatientInfo(int idpatient);

    }
}