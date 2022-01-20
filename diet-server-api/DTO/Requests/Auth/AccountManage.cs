using System.ComponentModel.DataAnnotations;

namespace diet_server_api.DTO.Requests.Auth
{
    public class AccountManage
    {
        [Required]
        public int IdUser { get; set; }
    }
}