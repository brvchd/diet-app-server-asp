using System;
using System.Collections.Generic;

#nullable disable

namespace diet_server_api.Models
{
    public partial class User
    {
        public int Iduser { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime Dateofbirth { get; set; }
        public string Email { get; set; }
        public string Phonenumber { get; set; }
        public string Pesel { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Refreshtoken { get; set; }
        public DateTime? Refreshtokenexp { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
