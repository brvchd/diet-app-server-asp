using System;
using System.Collections.Generic;

#nullable disable

namespace diet_server_api.Models
{
    public partial class Disease
    {
        public Disease()
        {
            DiseasePatients = new HashSet<DiseasePatient>();
        }

        public int Iddisease { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Recomendation { get; set; }

        public virtual ICollection<DiseasePatient> DiseasePatients { get; set; }
    }
}
