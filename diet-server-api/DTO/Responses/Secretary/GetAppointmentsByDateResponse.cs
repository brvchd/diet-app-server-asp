using System;

namespace diet_server_api.DTO.Responses.Secretary
{
    public class GetAppointmentsByDateResponse
    {
        public int IdVisit { get; set; }
        public string DoctorFullName { get; set; }
        public string PatientFullName { get; set; }
        public string TimeToDisplay { get; set; }
    }
}