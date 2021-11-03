using System;
using System.Collections.Generic;

#nullable disable

namespace diet_server_api.Models
{
    public partial class Productdiet
    {
        public int IdproductDiet { get; set; }
        public int Idproduct { get; set; }
        public int Iddiet { get; set; }
        public bool Allowed { get; set; }

        public virtual Diet IddietNavigation { get; set; }
        public virtual Product IdproductNavigation { get; set; }
    }
}
