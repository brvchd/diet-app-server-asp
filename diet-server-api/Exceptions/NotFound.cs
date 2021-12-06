using System;

namespace diet_server_api.Exceptions
{
    public class NotFound : Exception
    {
        public NotFound(string message) : base(message)
        {
        }
    }
}