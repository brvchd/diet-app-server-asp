using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace diet_server_api.DTO.Responses.Diet
{
    public class GetPatientDietResponse
    {
        public int IdDiet { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public decimal DailyMeals { get; set; }
        public decimal Protein { get; set; }
        public DateTime? ChangesDate { get; set; }
    }
}