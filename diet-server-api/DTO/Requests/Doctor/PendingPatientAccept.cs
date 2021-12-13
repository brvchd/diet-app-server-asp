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
        public decimal CorrectedValue { get; set; }
    }
}