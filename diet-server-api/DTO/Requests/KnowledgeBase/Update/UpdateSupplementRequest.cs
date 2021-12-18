using System.ComponentModel.DataAnnotations;

namespace diet_server_api.DTO.Requests.KnowledgeBase.Update
{
    public class UpdateSupplementRequest
    {
        [Required]
        public int IdSupplement { get; set; }
        [MaxLength(30)]
        public string Name { get; set; }
        [MaxLength(150)]
        public string Description { get; set; }
    }
}