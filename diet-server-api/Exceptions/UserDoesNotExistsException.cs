using System;

namespace diet_server_api.Exceptions
{
    public class UserDoesNotExistsException : Exception
    {
        public UserDoesNotExistsException(string message = "User does not exist") : base(message)
        {
        }
    }
}