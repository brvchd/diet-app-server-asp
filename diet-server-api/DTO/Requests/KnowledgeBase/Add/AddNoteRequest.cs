using System.ComponentModel.DataAnnotations;

namespace diet_server_api.DTO.Requests.KnowledgeBase.Add
{
    public class AddNoteRequest
    {
        [Required]
        public int IdPatient { get; set; }
        [Required]
        public int IdDoctor { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Note { get; set; }
    }
}