using System.ComponentModel.DataAnnotations;

namespace diet_server_api.DTO.Requests.KnowledgeBase.Add
{
    public class AddSupplementRequest
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        [MaxLength(150)]
        public string Description { get; set; }
    }
}