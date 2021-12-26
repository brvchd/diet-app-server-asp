using System;
using System.Collections.Generic;

#nullable disable

namespace diet_server_api.Models
{
    public partial class Day
    {
        public Day()
        {
            Mealtakes = new HashSet<Mealtake>();
        }

        public int Idday { get; set; }
        public int Daynumber { get; set; }
        public int Dietiddiet { get; set; }
        public string Patientreport { get; set; }

        public virtual Diet DietiddietNavigation { get; set; }
        public virtual ICollection<Mealtake> Mealtakes { get; set; }
    }
}
