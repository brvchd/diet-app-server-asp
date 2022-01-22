using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace diet_server_api.DTO.Requests.Diet
{
    public class AssignMealsRequest
    {
        [Required]
        public int IdDiet { get; set; }
        [Required]
        public int DayNumber { get; set; }
        [Required]
        public List<Meal> Meals { get; set; }
        public class Meal
        {
            [Required]
            public int IdMeal { get; set; }
            [Required]
            [MinLength(5)]
            [MaxLength(15)]
            public string Time { get; set; }
            [Required]
            public decimal Proportion { get; set; }

        }
    }
}