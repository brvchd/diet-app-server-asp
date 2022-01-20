using System;

namespace diet_server_api.DTO.Responses.Secretary
{
    public class GetAppointmentDetails
    {
        public string DoctorFullName { get; set; }
        public string PatientFullName { get; set; }
        public string PatientEmail { get; set; }
        public string PatientDateOfBirth { get; set; }
        public string AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }
        public string Description { get; set; }
    }
}