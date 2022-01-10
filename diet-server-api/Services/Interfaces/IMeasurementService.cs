using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using diet_server_api.DTO.Requests.Patient;
using diet_server_api.DTO.Responses.Patient;

namespace diet_server_api.Services.Interfaces.Repository
{
    public interface IMeasurementService
    {
        Task<AddMeasrumentsResponse> AddMeasurements(AddMeasrumentsRequest request, string whomeasured);
        Task<List<GetMeasurementsResponse>> GetMeasurements(int idPatient);
        Task<List<GetMeasurementsByDateResponse>> GetMeasurements(int idPatient, DateTime date, string whomeasured);
        Task<List<GetMeasurementResponse>> GetMeasurement(int idPatient);
    }
}