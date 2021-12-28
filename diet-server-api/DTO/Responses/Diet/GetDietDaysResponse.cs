using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace diet_server_api.DTO.Responses.Diet
{
    public class GetDietDaysResponse
    {
        public int Days { get; set; }
        public decimal TotalMeals { get; set; }
    }
}