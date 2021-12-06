using System.ComponentModel.DataAnnotations;

namespace diet_server_api.DTO.Requests.KnowledgeBase.Add
{
    public class AddParameterRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string MeasureUnit { get; set; }
    }
}