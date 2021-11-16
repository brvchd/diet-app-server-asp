using System;

namespace diet_server_api.Exceptions
{
    public class IncorrectGender : Exception
    {
        public IncorrectGender(string message = "Incorrect gender") : base(message)
        {
        }
    }
}