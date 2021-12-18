using System;
using System.ComponentModel.DataAnnotations;

namespace diet_server_api.DTO.Requests.Patient
{
    public class AddMeasrumentsRequest
    {
        [Required]
        public int Idpatient { get; set; }
        [Required]
        public decimal Height { get; set; }
        [Required]
        public decimal Weight { get; set; }
        [Required]
        public decimal Hipcircumference { get; set; }
        [Required]
        public decimal Waistcircumference { get; set; }
        [Required]
        public decimal Bicepscircumference { get; set; }
        [Required]
        public decimal Chestcircumference { get; set; }
        [Required]
        public decimal Thighcircumference { get; set; }
        [Required]
        public decimal Calfcircumference { get; set; }
        [Required]
        public decimal Waistlowercircumference { get; set; }
    }
}