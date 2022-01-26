using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace diet_server_api.DTO.Responses.Diet
{
    public class GetMidInfoResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string PESEL { get; set; }
        public bool Diabetes { get; set; }
        public bool Hypertension { get; set; }
        public bool InsulinResistance { get; set; }
        public bool Hypothyroidism { get; set; }
        public bool IntestinalDiseases { get; set; }
        public string OtherDiseases { get; set; }
        public string CurrentDiet { get; set; }
    }
}