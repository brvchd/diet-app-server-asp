using System;

namespace diet_server_api.DTO.Responses.Patient
{
    public class GetPatientAppointmentDetailsResponse
    {
        public string DoctorFullName { get; set; }
        public string Email { get; set; }
        public string Office { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Description { get; set; }
    }
}