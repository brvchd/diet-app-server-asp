using System;
using System.Collections.Generic;

#nullable disable

namespace diet_server_api.Models
{
    public partial class Mealsbeforediet
    {
        public int Idmeal { get; set; }
        public int Idquestionary { get; set; }
        public int Mealnumber { get; set; }
        public string Hour { get; set; }
        public string Foodtoeat { get; set; }

        public virtual Questionary IdquestionaryNavigation { get; set; }
    }
}
