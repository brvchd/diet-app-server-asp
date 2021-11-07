using System;
using System.Collections.Generic;

#nullable disable

namespace diet_server_api.Models
{
    public partial class TempUser
    {
        public string Email { get; set; }
        public string Uniquekey { get; set; }
        public string Token { get; set; }
    }
}
