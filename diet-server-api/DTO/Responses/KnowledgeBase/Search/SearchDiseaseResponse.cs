namespace diet_server_api.DTO.Responses.KnowledgeBase
{
    public class SearchDiseaseResponse
    {
        public int IdDisease { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Recommendation { get; set; }
    }
}