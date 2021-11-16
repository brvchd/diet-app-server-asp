using System.ComponentModel.DataAnnotations;

namespace diet_server_api.DTO.Requests.Doctor
{
    public class PendingPatientDataRequest
    {
        [Required]
        public int UserId { get; set; }
    }
}