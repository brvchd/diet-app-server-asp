using System.ComponentModel.DataAnnotations;

namespace diet_server_api.DTO.Requests.KnowledgeBase.Add
{
    public class AddDiseaseRequest
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        [MaxLength(15000)]
        public string Description { get; set; }
        [Required]
        [MaxLength(15000)]
        public string Recomendation { get; set; }
    }
}