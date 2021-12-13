using System;
using System.Collections.Generic;

#nullable disable

namespace diet_server_api.Models
{
    public partial class Patient
    {
        public Patient()
        {
            Diets = new HashSet<Diet>();
            DiseasePatients = new HashSet<DiseasePatient>();
            Foodinputs = new HashSet<Foodinput>();
            Measurements = new HashSet<Measurement>();
            Notes = new HashSet<Note>();
            Questionaries = new HashSet<Questionary>();
            Visits = new HashSet<Visit>();
        }

        public int Iduser { get; set; }
        public bool Ispending { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Flatnumber { get; set; }
        public decimal? Pal { get; set; }
        public string Streetnumber { get; set; }
        public decimal? Correctedvalue { get; set; }
        public decimal? Cpm { get; set; }

        public virtual User IduserNavigation { get; set; }
        public virtual ICollection<Diet> Diets { get; set; }
        public virtual ICollection<DiseasePatient> DiseasePatients { get; set; }
        public virtual ICollection<Foodinput> Foodinputs { get; set; }
        public virtual ICollection<Measurement> Measurements { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<Questionary> Questionaries { get; set; }
        public virtual ICollection<Visit> Visits { get; set; }
    }
}
