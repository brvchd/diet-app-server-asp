namespace diet_server_api.DTO.Responses.Doctor
{
    public class SearchDiseaseResponse
    {
        public int IdDisease { get; set; }
        public string DiseaseName { get; set; }
        public string Description { get; set; }
        public string Recommendation { get; set; }
    }
}