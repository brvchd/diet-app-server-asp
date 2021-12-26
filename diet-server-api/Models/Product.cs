using System;
using System.Collections.Generic;

#nullable disable

namespace diet_server_api.Models
{
    public partial class Product
    {
        public Product()
        {
            ProductParameters = new HashSet<ProductParameter>();
            Recipes = new HashSet<Recipe>();
        }

        public int Idproduct { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public decimal Size { get; set; }
        public string Homemeasure { get; set; }
        public decimal Homemeasuresize { get; set; }

        public virtual ICollection<ProductParameter> ProductParameters { get; set; }
        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
