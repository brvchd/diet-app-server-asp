using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace diet_server_api.DTO.Requests.Diet
{
    public class CreateDietRequest
    {
        [Required]
        public int IdPatient { get; set; }
        [Required]
        public int IdDoctor { get; set; }
        [Required]
        [MinLength(1)]
        public string Name { get; set; }
        [Required]
        [MinLength(10)]
        public string Description { get; set; }
        [Required]
        public DateTime DateFrom { get; set; }
        [Required]
        public DateTime DateTo { get; set; }
        [Required]
        public decimal DailyMeals { get; set; }
        [Required]
        public decimal Protein { get; set; }
        [Required]
        public List<Supplement> Supplements { get; set; }
        public class Supplement
        {
            [Required]
            public int IdSupplement { get; set; }
            [Required]
            [MinLength(1)]
            public string DietSupplDescription { get; set; }
        }

    }
}