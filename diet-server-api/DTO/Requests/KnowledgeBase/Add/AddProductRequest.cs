using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace diet_server_api.DTO.Requests.KnowledgeBase.Add
{
    public class AddProductRequest
    {
        [Required]
        [MinLength(1)]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string Unit { get; set; }
        [Required]
        public decimal Size { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string HomeMeasure { get; set; }
        [Required]
        public decimal HomeMeasureSize { get; set; }
        [Required]
        [MinLength(1)]
        public List<Parameter> Parameters { get; set; }

        public class Parameter
        {
            [Required]
            public int IdParameter { get; set; }
            [Required]
            public decimal Amount { get; set; }
        }


    }
}