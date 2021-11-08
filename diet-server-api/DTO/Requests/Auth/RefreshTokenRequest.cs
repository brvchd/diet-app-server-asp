using System.ComponentModel.DataAnnotations;

namespace diet_server_api.DTO.Requests.Auth
{
    public class RefreshTokenRequest
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}