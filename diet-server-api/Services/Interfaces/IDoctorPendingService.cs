using System.Collections.Generic;
using System.Threading.Tasks;
using diet_server_api.DTO.Requests.Doctor;
using diet_server_api.DTO.Responses.Doctor;

namespace diet_server_api.Services.Interfaces
{
    public interface IDoctorPendingService
    {
        Task<List<PendingPatientResponse>> GetPatients();
        Task<PendingPatientDataResponse> GetPatientData(int id);
        Task AcceptPatient(PendingPatientAccept request);
        Task RejectPendingPatient(int idPatient);
    }
}