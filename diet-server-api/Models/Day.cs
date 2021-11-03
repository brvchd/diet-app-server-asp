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
        public int DietIddiet { get; set; }

        public virtual Diet DietIddietNavigation { get; set; }
        public virtual ICollection<Mealtake> Mealtakes { get; set; }
    }
}
