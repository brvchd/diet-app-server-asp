using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using diet_server_api.DTO.Requests.Secretary;
using diet_server_api.DTO.Responses.Secretary;

namespace diet_server_api.Services.Interfaces
{
    public interface ISecretaryService
    {
        Task AssignAppointment(AssignAppointmentRequest request);
        Task SendEmail(SendEmailRequest request);
        Task<List<SearchUserResponse>> SearchPatient(string firstname, string lastname);
        Task<List<SearchUserResponse>> SearchDoctor(string firstname, string lastname);
        Task<List<GetAppointmentsByDateResponse>> GetAppointmentsByDates(DateTime date);
        Task<List<DateTime>> GetAppointmentDates();
        Task<GetAppointmentDetails> GetAppointmentDetails(int idVisit);
        Task CancelAppointment(int idVisit);
    }
}