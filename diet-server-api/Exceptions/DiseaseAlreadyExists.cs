using System;

namespace diet_server_api.Exceptions
{
    public class DiseaseAlreadyExists : Exception
    {
        public DiseaseAlreadyExists(string message = "Disease already exists") : base(message)
        {
        }
    }
}