using System;

namespace diet_server_api.Exceptions
{
    public class IncorrectCredentials : Exception
    {
        public IncorrectCredentials(string message = "Credentials are incorrect!") : base(message)
        {
        }
    }
}