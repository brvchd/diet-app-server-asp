using System.Collections.Generic;

namespace diet_server_api.DTO.Responses.KnowledgeBase.Get
{
    public class GetSupplementsResponse
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalRows { get; set; }
        public List<Supplement> Supplements { get; set; }
        public class Supplement
        {
            public int IdSupplement { get; set; }
            public string SupplementName { get; set; }
            public string Description { get; set; }
        }
    }
}