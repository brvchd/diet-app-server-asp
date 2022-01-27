using System;
using System.Collections.Generic;

namespace diet_server_api.DTO.Requests.Patient
{
    public class GetPatientReport
    {
        public MeasurementReport InitialReport { get; set; }
        public List<MeasurementReport> NewReports { get; set; }
        public class MeasurementReport
        {
            public decimal Height { get; set; }
            public decimal Weight { get; set; }
            public DateTime Date { get; set; }
            public decimal? HipCircum { get; set; }
            public decimal? WaistCircum { get; set; }
            public decimal? BicepsCircum { get; set; }
            public decimal? ChestCircum { get; set; }
            public decimal? ThighCircum { get; set; }
            public decimal? CalfCircum { get; set; }
            public decimal? WaistLowerCircum { get; set; }
            public string WhoMeasured { get; set; }
        }
        public class DayReport
        {
            public string DietName { get; set; }
            public int DayNumber { get; set; }
            public string PatientReport { get; set; }
            public List<MealTakeReport> Meals { get; set; }
        }
        public class MealTakeReport
        {
            public string Time { get; set; }
            public bool? IsFollowed { get; set; }
        }
    }

}