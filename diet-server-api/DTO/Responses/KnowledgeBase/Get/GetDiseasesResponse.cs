using System.Collections.Generic;

namespace diet_server_api.DTO.Responses.KnowledgeBase.Get
{
    public class GetDiseasesResponse
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalRows { get; set; }
        public List<Disease> Diseases { get; set; }
        public class Disease
        {
            public int IdDisease { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Recomendation { get; set; }
        }
    }
}