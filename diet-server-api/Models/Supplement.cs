using System;
using System.Collections.Generic;

#nullable disable

namespace diet_server_api.Models
{
    public partial class Supplement
    {
        public Supplement()
        {
            Dietsuppliments = new HashSet<Dietsuppliment>();
        }

        public int Idsuppliment { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Dietsuppliment> Dietsuppliments { get; set; }
    }
}
