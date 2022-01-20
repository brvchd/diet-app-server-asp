using System;

namespace diet_server_api.DTO.Responses.Admin
{
    public class SearchUsersResponse
    {
        public int IdUser { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PESEL { get; set; }
        public string Role { get; set; }
        public string Office { get; set; }
        public bool IsActive { get; set; }
    }
}