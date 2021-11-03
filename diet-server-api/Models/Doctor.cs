using System;
using System.Collections.Generic;

#nullable disable

namespace diet_server_api.Models
{
    public partial class Doctor
    {
        public Doctor()
        {
            Notes = new HashSet<Note>();
            Visits = new HashSet<Visit>();
        }

        public int Iduser { get; set; }
        public string Office { get; set; }

        public virtual User IduserNavigation { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<Visit> Visits { get; set; }
    }
}
