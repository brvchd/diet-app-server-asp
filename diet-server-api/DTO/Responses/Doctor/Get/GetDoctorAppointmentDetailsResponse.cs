using System;

namespace diet_server_api.DTO.Responses.Doctor.Get
{
    public class GetDoctorAppointmentDetailsResponse
    {
        public string PatientFullName { get; set; }
        public string Email { get; set; }
        public string DateOfBirth { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Description { get; set; }
    }
}   