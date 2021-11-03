using System;
using System.Collections.Generic;

#nullable disable

namespace diet_server_api.Models
{
    public partial class Meal
    {
        public Meal()
        {
            Recipes = new HashSet<Recipe>();
        }

        public int Idmeal { get; set; }
        public string Nameofmeal { get; set; }
        public string Description { get; set; }
        public string CookingUrl { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
