using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace diet_server_api.DTO.Responses.KnowledgeBase.Get
{
    public class GetPatientDiseasesResponse
    {
        public int IdPatientDisease { get; set; }
        public int IdDisease { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Recommendation { get; set; }
        public DateTime? DateOfDiagnosis { get; set; }
        public DateTime? DateOfCure { get; set; }
    }
}