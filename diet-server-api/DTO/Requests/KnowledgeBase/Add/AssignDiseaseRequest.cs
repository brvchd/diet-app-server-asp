using System;
using System.ComponentModel.DataAnnotations;

namespace diet_server_api.DTO.Requests.KnowledgeBase.Add
{
    public class AssignDiseaseRequest
    {
        [Required]
        public int IdPatient { get; set; }
        [Required]
        public int IdDisease { get; set; }
        [Required]
        public DateTime DateOfDiagnosis { get; set; }
        public DateTime? DateOfCure { get; set; }
    }
}