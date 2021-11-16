using System.ComponentModel.DataAnnotations;

namespace diet_server_api.DTO.Requests.Admin
{
    public class TemporaryUserRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string UniqueKey { get; set; }
    }
}
