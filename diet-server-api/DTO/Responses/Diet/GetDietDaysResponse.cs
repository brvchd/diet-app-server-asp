using System.Collections.Generic;

namespace diet_server_api.DTO.Responses.Diet
{
    public class GetDietDaysResponse
    {
        public int Days { get; set; }
        public decimal TotalMeals { get; set; }
        public decimal Proteins { get; set; }
        public int DaysFilled { get; set; }
        public List<int> DaysNumberFilled { get; set; }
    }
}