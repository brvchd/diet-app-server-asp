using System;

namespace diet_server_api.Exceptions
{
    public class UserNotFound : Exception
    {
        public UserNotFound(string message = "User does not exist") : base(message)
        {
        }
    }
}