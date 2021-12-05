using System;

namespace diet_server_api.Exceptions
{
    public class SupplementNotFound : Exception
    {
        public SupplementNotFound(string message = "Supplement not found") : base(message)
        {

        }
    }
}