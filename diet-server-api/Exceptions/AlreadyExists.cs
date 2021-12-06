using System;

namespace diet_server_api.Exceptions
{
    public class AlreadyExists : Exception
    {
        public AlreadyExists(string message) : base(message)
        {
        }
    }
}