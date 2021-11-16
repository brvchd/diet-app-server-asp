using System;

namespace diet_server_api.Exceptions
{
    public class SupplementAlreadyExists : Exception
    {
        public SupplementAlreadyExists(string message = "Supplement already exists") : base(message)
        {
        }
    }
}