using System;
using System.Collections.Generic;

#nullable disable

namespace diet_server_api.Models
{
    public partial class Visit
    {
        public int Idvisit { get; set; }
        public int Iddoctor { get; set; }
        public int Idpatient { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Description { get; set; }

        public virtual Doctor IddoctorNavigation { get; set; }
        public virtual Patient IdpatientNavigation { get; set; }
    }
}
