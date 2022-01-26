using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace diet_server_api.DTO.Requests.Patient
{
    public class FillDayReportRequest
    {
        [Required]
        public int IdDay { get; set; }
        [Required]
        public string PatientReport { get; set; }
        [MinLength(1)]
        [Required]
        public List<MealFollowed> Meals { get; set; }

    }
    public class MealFollowed
    {
        [Required]
        public int IdMealTake { get; set; }
        [Required]
        public bool IsFollowed { get; set; }
    }
}