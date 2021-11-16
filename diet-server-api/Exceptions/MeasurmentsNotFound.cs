using System;

namespace diet_server_api.Exceptions
{
    public class MeasurmentsNotFound : Exception
    {
        public MeasurmentsNotFound(string message = "Measurments for requested patient were not found") : base(message)
        {
        }
    }
}