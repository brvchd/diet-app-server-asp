using System.Collections.Generic;
using System.Threading.Tasks;
using diet_server_api.DTO.Requests.Doctor;
using diet_server_api.DTO.Responses.Doctor.Get;

namespace diet_server_api.Services.Interfaces
{
    public interface IDoctorPendingService
    {
        Task<List<PendingPatientResponse>> GetPendingPatients();
        Task<PendingPatientDataResponse> GetPendingPatientData(int id);
        Task AcceptPendingPatient(PendingPatientAccept request);
        Task RejectPendingPatient(int idPatient);
    }
}