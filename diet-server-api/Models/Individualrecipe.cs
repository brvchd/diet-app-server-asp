using System;
using System.Collections.Generic;

#nullable disable

namespace diet_server_api.Models
{
    public partial class Individualrecipe
    {
        public int Idindividualrecipe { get; set; }
        public int Idrecipe { get; set; }
        public decimal Proportion { get; set; }
        public int Idmealtake { get; set; }

        public virtual Mealtake IdmealtakeNavigation { get; set; }
        public virtual Recipe IdrecipeNavigation { get; set; }
    }
}
