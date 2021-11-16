using System.ComponentModel.DataAnnotations;

namespace diet_server_api.DTO.Requests.Admin
{
    public class TemporaryUserRequest
    {
        [Required]
        [EmailAddress]
        [MaxLength(60)]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        [MaxLength(50)]
        public string UniqueKey { get; set; }
    }
}
