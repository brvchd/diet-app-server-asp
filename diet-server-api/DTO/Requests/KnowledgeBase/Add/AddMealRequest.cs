using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace diet_server_api.DTO.Requests.KnowledgeBase.Add
{
    public class AddMealRequest
    {
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string NameOfMeal { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(15000)]
        public string Description { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(300)]
        public string CookingURL { get; set; }
        [Required]
        [MinLength(1)]
        public List<Recipe> Recipes { get; set; }
        public class Recipe
        {
            [Required]
            public int IdProduct { get; set; }
            [Required]
            public decimal Amount { get; set; }
        }
    }
}