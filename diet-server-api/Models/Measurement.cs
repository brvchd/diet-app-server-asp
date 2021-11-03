using System;
using System.Collections.Generic;

#nullable disable

namespace diet_server_api.Models
{
    public partial class Measurement
    {
        public int Idmeasurement { get; set; }
        public int Idpatient { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public DateTime Date { get; set; }
        public decimal Hipcircumference { get; set; }
        public decimal Waistcircumference { get; set; }
        public int? Bicepscircumference { get; set; }
        public int? Chestcircumference { get; set; }
        public int? Thighcircumference { get; set; }
        public int? Calfcircumference { get; set; }
        public int? Waistlowercircumference { get; set; }
        public string Whomeasured { get; set; }

        public virtual Patient IdpatientNavigation { get; set; }
    }
}
