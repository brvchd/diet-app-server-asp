using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using diet_server_api.DTO.Responses.Doctor.Get;
using diet_server_api.DTO.Responses.Patient;

namespace diet_server_api.Services.Interfaces
{
    public interface IAppointmentsService
    {
        Task<List<DateTime>> GetPatientAppointmentDates(int idPatient);
        Task<List<DateTime>> GetDoctorAppointmentDates(int idDoctor);
        Task<GetDoctorAppointmentDetailsResponse> GetDoctorAppointmentDetails(int idVisit);
        Task<GetPatientAppointmentDetailsResponse> GetPatientAppointmentDetails(int idVisit);
        Task<List<GetDoctorAppointmentsByDateResponse>> GetDoctorAppointmentsByDates(DateTime date, int idDoctor);
        Task<List<GetPatientAppointmentsByDateResponse>> GetPatientAppointmentsByDates(int idPatient);
    }
}