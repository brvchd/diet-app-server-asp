using System;
using System.ComponentModel.DataAnnotations;

namespace diet_server_api.DTO.Requests.Admin
{
    public class DoctorCreatorRequest
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        [MaxLength(11)]
        public string PESEL { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Office { get; set; }
    }
}