using System;
using System.Collections.Generic;

#nullable disable

namespace diet_server_api.Models
{
    public partial class Recipe
    {
        public Recipe()
        {
            Individualrecipes = new HashSet<Individualrecipe>();
        }

        public int Idrecipe { get; set; }
        public int Idproduct { get; set; }
        public int Idmeal { get; set; }
        public int Amount { get; set; }

        public virtual Meal IdmealNavigation { get; set; }
        public virtual Product IdproductNavigation { get; set; }
        public virtual ICollection<Individualrecipe> Individualrecipes { get; set; }
    }
}
