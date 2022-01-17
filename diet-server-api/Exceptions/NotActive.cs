using System;

namespace diet_server_api.Exceptions
{
    public class NotActive : Exception
    {
        public NotActive(string message) : base(message)
        {
        }
    }
}