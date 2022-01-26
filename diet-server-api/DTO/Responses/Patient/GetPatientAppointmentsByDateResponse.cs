using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace diet_server_api.DTO.Responses.Patient
{
    public class GetPatientAppointmentsByDateResponse
    {
        public int IdVisit { get; set; }
        public string DoctorFullName { get; set; }
        public string TimeToDisplay { get; set; }
        public DateTime Date { get; set; }
    }
}