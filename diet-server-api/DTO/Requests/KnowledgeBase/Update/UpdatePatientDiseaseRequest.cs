using System;
using System.ComponentModel.DataAnnotations;

namespace diet_server_api.DTO.Requests.KnowledgeBase.Update
{
    public class UpdatePatientDiseaseRequest
    {
        [Required]
        public int IdPatientDisease { get; set; }
        public DateTime? DateOfCure { get; set; }
        public DateTime? DateOfDiagnosis { get; set; }
    }
}