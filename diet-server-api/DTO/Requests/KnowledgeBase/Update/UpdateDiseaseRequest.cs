using System.ComponentModel.DataAnnotations;

namespace diet_server_api.DTO.Requests.KnowledgeBase.Update
{
    public class UpdateDiseaseRequest
    {
        [Required]
        public int IdDisease { get; set; }
        [MaxLength(30)]
        public string Name { get; set; }
        [MaxLength(15000)]
        public string Description { get; set; }
        [MaxLength(15000)]
        public string Recomendation { get; set; }
    }
}