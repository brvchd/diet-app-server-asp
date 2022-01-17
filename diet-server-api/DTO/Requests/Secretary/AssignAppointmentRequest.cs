using System;
using System.ComponentModel.DataAnnotations;

namespace diet_server_api.DTO.Requests.Secretary
{
    public class AssignAppointmentRequest
    {
        [Required]
        public int IdDoctor { get; set; }
        [Required]
        public int IdPatient { get; set; }
        [Required]
        public DateTime AppointmentDate { get; set; }
        [Required]
        public string Description { get; set; }
    }
}