using System;
using System.Collections.Generic;

#nullable disable

namespace diet_server_api.Models
{
    public partial class Foodinput
    {
        public int Idinput { get; set; }
        public int Idproduct { get; set; }
        public int Idpatient { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public string Unit { get; set; }
        public TimeSpan Time { get; set; }
        public DateTime Date { get; set; }

        public virtual Patient IdpatientNavigation { get; set; }
        public virtual Product IdproductNavigation { get; set; }
    }
}
