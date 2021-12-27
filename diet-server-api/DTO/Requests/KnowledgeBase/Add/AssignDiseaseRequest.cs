using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace diet_server_api.DTO.Requests.KnowledgeBase.Add
{
    public class AssignDiseaseRequest
    {
        [Required]
        public int IdPatient { get; set; }
        [Required]
        public int IdDisease { get; set; }
        public DateTime DateOfDiagnosis { get; set; }
    }
}