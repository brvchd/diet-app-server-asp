using System.ComponentModel.DataAnnotations;

namespace diet_server_api.DTO.Requests
{
    public class SurveyCheckCredentialsRequest
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Unique key is required")]
        public string UniqueKey { get; set; }
    }
}