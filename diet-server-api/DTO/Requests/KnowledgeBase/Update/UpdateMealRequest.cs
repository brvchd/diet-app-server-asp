using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace diet_server_api.DTO.Requests.KnowledgeBase.Update
{
    public class UpdateMealRequest
    {
        [Required]
        public int IdMeal { get; set; }
        [MaxLength(15000)]
        public string Description { get; set; }
        [MaxLength(200)]
        public string CookingURL { get; set; }
        [MaxLength(50)]
        public string MealName { get; set; }
    }
}