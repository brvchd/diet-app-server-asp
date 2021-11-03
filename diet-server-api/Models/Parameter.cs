using System;
using System.Collections.Generic;

#nullable disable

namespace diet_server_api.Models
{
    public partial class Parameter
    {
        public Parameter()
        {
            ProductParameters = new HashSet<ProductParameter>();
        }

        public int Idparameter { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }

        public virtual ICollection<ProductParameter> ProductParameters { get; set; }
    }
}
