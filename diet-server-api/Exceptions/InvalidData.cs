using System;

namespace diet_server_api.Exceptions
{
    public class InvalidData : Exception
    {
        public InvalidData(string message = "Credentials are incorrect!") : base(message)
        {
        }
    }
}