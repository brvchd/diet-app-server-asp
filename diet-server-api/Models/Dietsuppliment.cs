using System;
using System.Collections.Generic;

#nullable disable

namespace diet_server_api.Models
{
    public partial class Dietsuppliment
    {
        public int Iddietsuppliment { get; set; }
        public int Iddiet { get; set; }
        public int Idsuppliment { get; set; }
        public int Dose { get; set; }
        public string Description { get; set; }

        public virtual Diet IddietNavigation { get; set; }
        public virtual Supplement IdsupplimentNavigation { get; set; }
    }
}
