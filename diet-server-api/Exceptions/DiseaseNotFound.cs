using System;

namespace diet_server_api.Exceptions
{
    public class DiseaseNotFound : Exception
    {
        public DiseaseNotFound(string message = "Disease not found") : base(message)
        {
        }
    }
}