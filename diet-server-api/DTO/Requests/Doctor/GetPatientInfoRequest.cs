using System.ComponentModel.DataAnnotations;

namespace diet_server_api.DTO.Requests.Doctor
{
    public class GetPatientInfoRequest
    {
        [Required]
        public int IdPatient { get; set; }
    }
}