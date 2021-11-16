using System.ComponentModel.DataAnnotations;

namespace diet_server_api.DTO.Requests.Doctor
{
    public class PendingPatientAccept
    {
        [Required]
        public int IdPatient { get; set; }
        [Required]
        public decimal CPM { get; set; }
        [Required]
        public decimal PAL { get; set; }
        [Required]
        [MaxLength(70)]
        public string CorrectedValue { get; set; }
    }
}