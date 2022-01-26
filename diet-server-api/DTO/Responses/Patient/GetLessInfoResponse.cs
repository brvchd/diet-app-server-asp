using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace diet_server_api.DTO.Responses.Diet
{
    public class GetLessInfoResponse
    {
         public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string PESEL { get; set; }
        public string CurrentDietName { get; set; }
        public string SelectedDiet { get; set; }
    }
}