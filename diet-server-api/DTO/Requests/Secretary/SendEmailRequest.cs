using System.ComponentModel.DataAnnotations;

namespace diet_server_api.DTO.Requests.Secretary
{
    public class SendEmailRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string FullName { get; set; }
    }
}