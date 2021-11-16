using System.Collections.Generic;
using System.Threading.Tasks;
using diet_server_api.DTO.Requests.Doctor;
using diet_server_api.DTO.Responses.Doctor;

namespace diet_server_api.Services.Interfaces
{
    public interface IDoctorPendingService
    {
        public Task<List<PendingPatientResponse>> GetPatients();
        public Task<PendingPatientDataResponse> GetPatientData(int id);
        public Task AcceptPatient(PendingPatientAccept request);
    }
}