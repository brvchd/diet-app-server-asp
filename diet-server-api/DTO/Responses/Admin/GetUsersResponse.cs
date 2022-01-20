using System;
using System.Collections.Generic;

namespace diet_server_api.DTO.Responses.Admin
{
    public class GetUsersResponse
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalRows { get; set; }
        public List<Employee> Employees { get; set; }

        public class Employee
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
            public string PhoneNumber { get; set; }

        }
    }
}