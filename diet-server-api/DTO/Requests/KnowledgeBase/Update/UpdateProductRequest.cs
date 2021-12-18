using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace diet_server_api.DTO.Requests.KnowledgeBase.Update
{
    public class UpdateProductRequest
    {
        [Required]
        public int ProductId { get; set; }
        [MaxLength(30)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Unit { get; set; }
        public decimal Size { get; set; }
        [MaxLength(50)]
        public string HomeMeasure { get; set; }
        public decimal HomeMeasureSize { get; set; }
        [Required]
        [MinLength(2)]
        public List<Param> Parameters { get; set; }
        public class Param
        {
            [Required]
            public int IdParameter { get; set; }
            [Required]
            public decimal Amount { get; set; }
        }

    }
}