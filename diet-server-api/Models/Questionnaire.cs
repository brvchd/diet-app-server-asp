using System;
using System.Collections.Generic;

#nullable disable

namespace diet_server_api.Models
{
    public partial class Questionnaire
    {
        public Questionnaire()
        {
            Mealsbeforediets = new HashSet<Mealsbeforediet>();
        }

        public int Idquestionary { get; set; }
        public int Idpatient { get; set; }
        public DateTime Databadania { get; set; }
        public string Education { get; set; }
        public string Profession { get; set; }
        public string Mainproblems { get; set; }
        public bool Hypertension { get; set; }
        public bool Insulinresistance { get; set; }
        public bool Diabetes { get; set; }
        public bool Hypothyroidism { get; set; }
        public bool Intestinaldiseases { get; set; }
        public string Otherdiseases { get; set; }
        public string Medications { get; set; }
        public string Supplementstaken { get; set; }
        public decimal Avgsleep { get; set; }
        public string Usuallywakeup { get; set; }
        public string Usuallygotosleep { get; set; }
        public bool Regularwalk { get; set; }
        public decimal Excercisingperday { get; set; }
        public string Sporttypes { get; set; }
        public decimal Exercisingperweek { get; set; }
        public int Waterglasses { get; set; }
        public int Coffeeglasses { get; set; }
        public int Teaglasses { get; set; }
        public int Juiceglasses { get; set; }
        public int Energydrinkglasses { get; set; }
        public string Alcoholinfo { get; set; }
        public int Cigs { get; set; }
        public bool Breakfast { get; set; }
        public bool Secondbreakfast { get; set; }
        public bool Lunch { get; set; }
        public bool Afternoonmeal { get; set; }
        public bool Dinner { get; set; }
        public string Favfooditems { get; set; }
        public string Notfavfooditems { get; set; }
        public string Hypersensitivityproducts { get; set; }
        public string Alergieproducts { get; set; }
        public string Betweenmealsfood { get; set; }

        public virtual Patient IdpatientNavigation { get; set; }
        public virtual ICollection<Mealsbeforediet> Mealsbeforediets { get; set; }
    }
}
