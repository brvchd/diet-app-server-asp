using System;

namespace diet_server_api.DTO.Responses.Doctor.Search
{
    public class PatientSearchResponse
    {
        public int IdPatient { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}