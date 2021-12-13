namespace diet_server_api.DTO.Requests.KnowledgeBase.Update
{
    public class UpdateSupplementRequest
    {
        public int IdSupplement { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}