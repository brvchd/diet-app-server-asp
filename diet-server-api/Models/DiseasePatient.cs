using System;
using System.Collections.Generic;

#nullable disable

namespace diet_server_api.Models
{
    public partial class DiseasePatient
    {
        public int IddiseasePatient { get; set; }
        public int Iddisease { get; set; }
        public int Idpatient { get; set; }
        public DateTime? Dateofdiagnosis { get; set; }
        public DateTime? Dateofcure { get; set; }

        public virtual Disease IddiseaseNavigation { get; set; }
        public virtual Patient IdpatientNavigation { get; set; }
    }
}
