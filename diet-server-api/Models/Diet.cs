using System;
using System.Collections.Generic;

#nullable disable

namespace diet_server_api.Models
{
    public partial class Diet
    {
        public Diet()
        {
            Days = new HashSet<Day>();
            Dietsuppliments = new HashSet<Dietsuppliment>();
            Productdiets = new HashSet<Productdiet>();
        }

        public int Iddiet { get; set; }
        public int Idpatient { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Datefrom { get; set; }
        public DateTime Dateto { get; set; }
        public DateTime? Datetoinformaboutchanges { get; set; }
        public int Numberofmealsperday { get; set; }
        public decimal Totalamountofcalories { get; set; }
        public int Carbs { get; set; }
        public int Fat { get; set; }
        public int Fiber { get; set; }
        public int Protein { get; set; }

        public virtual Patient IdpatientNavigation { get; set; }
        public virtual ICollection<Day> Days { get; set; }
        public virtual ICollection<Dietsuppliment> Dietsuppliments { get; set; }
        public virtual ICollection<Productdiet> Productdiets { get; set; }
    }
}
