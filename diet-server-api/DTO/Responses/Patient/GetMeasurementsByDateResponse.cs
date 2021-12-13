using System;

namespace diet_server_api.DTO.Responses.Patient
{
    public class GetMeasurementsByDateResponse
    {
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public DateTime Date { get; set; }
        public decimal Hipcircumference { get; set; }
        public decimal Waistcircumference { get; set; }
        public decimal? Bicepscircumference { get; set; }
        public decimal? Chestcircumference { get; set; }
        public decimal? Thighcircumference { get; set; }
        public decimal? Calfcircumference { get; set; }
        public decimal? Waistlowercircumference { get; set; }
        public string Whomeasured { get; set; }
    }
}