using System;

namespace diet_server_api.DTO.Responses.Doctor.Get
{
    public class GetDoctorAppointmentsByDateResponse
    {
        public int IdVisit { get; set; }
        public string PatientFullName { get; set; }
        public string TimeToDisplay { get; set; }
        public DateTime Date { get; set; }
    }
}