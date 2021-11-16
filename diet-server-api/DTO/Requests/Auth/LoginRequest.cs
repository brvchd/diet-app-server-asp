using System.ComponentModel.DataAnnotations;
namespace diet_server_api.DTO.Requests.Auth
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

    }
}