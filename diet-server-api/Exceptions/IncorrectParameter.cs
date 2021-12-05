using System;

namespace diet_server_api.Exceptions
{
    public class IncorrectParameter : Exception
    {
        public IncorrectParameter(string message = "Provided parameter is incorrect") : base(message)
        {
        }
    }
}