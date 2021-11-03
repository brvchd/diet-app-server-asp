using System;
using System.Collections.Generic;

#nullable disable

namespace diet_server_api.Models
{
    public partial class Note
    {
        public int Idnote { get; set; }
        public int Idpatient { get; set; }
        public int Iddoctor { get; set; }
        public DateTime Dateofnote { get; set; }

        public virtual Doctor IddoctorNavigation { get; set; }
        public virtual Patient IdpatientNavigation { get; set; }
    }
}
