namespace diet_server_api.DTO.Requests.KnowledgeBase.Update
{
    public class UpdateDiseaseRequest
    {
        public int IdDisease { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Recomendation { get; set; }
    }
}