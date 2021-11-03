using System;
using System.Collections.Generic;

#nullable disable

namespace diet_server_api.Models
{
    public partial class Product
    {
        public Product()
        {
            Foodinputs = new HashSet<Foodinput>();
            ProductParameters = new HashSet<ProductParameter>();
            Productdiets = new HashSet<Productdiet>();
            Recipes = new HashSet<Recipe>();
        }

        public int Idproduct { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public int Servingsizeingramms { get; set; }
        public int Homemeasure { get; set; }

        public virtual ICollection<Foodinput> Foodinputs { get; set; }
        public virtual ICollection<ProductParameter> ProductParameters { get; set; }
        public virtual ICollection<Productdiet> Productdiets { get; set; }
        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
