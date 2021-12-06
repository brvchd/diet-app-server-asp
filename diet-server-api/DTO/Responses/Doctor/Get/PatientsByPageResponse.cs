using System;
using System.Collections.Generic;

namespace diet_server_api.DTO.Responses.Doctor.Get
{
    public class PatientsByPageResponse
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalRows { get; set; }
        public List<PatientByPage> Patients { get; set; }
        public class PatientByPage
        {
            public int IdPatient { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime DateOfBirth { get; set; }
        }
    }
}