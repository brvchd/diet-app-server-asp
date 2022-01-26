using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using diet_server_api.DTO.Responses.Diet;

namespace diet_server_api.Services.Interfaces
{
    public interface IInformationService
    {
        Task<GetFullInfoResponse> GetFullPatientInfo(int idPatient, int? idDiet);
        Task<GetMidInfoResponse> GetMidPatientInfo(int idPatient);
        Task<GetLessInfoResponse> GetLessInfoResponse(int idPatient, int? idDiet);
    }
}