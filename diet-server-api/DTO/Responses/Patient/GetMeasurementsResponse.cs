using System;

namespace diet_server_api.DTO.Responses.Patient
{
    public class GetMeasurementsResponse
    {
        public int IdMeasurement { get; set; }
        public DateTime Date { get; set; }
    }
}