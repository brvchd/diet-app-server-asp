using System;
using System.Collections.Generic;

#nullable disable

namespace diet_server_api.Models
{
    public partial class ProductParameter
    {
        public int IdproductParameter { get; set; }
        public int Idproduct { get; set; }
        public int Idparameter { get; set; }
        public decimal Amount { get; set; }

        public virtual Parameter IdparameterNavigation { get; set; }
        public virtual Product IdproductNavigation { get; set; }
    }
}
