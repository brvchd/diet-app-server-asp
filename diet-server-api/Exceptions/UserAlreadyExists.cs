using System;

namespace diet_server_api.Exceptions
{
    public class UserAlreadyExists : Exception
    {
        public UserAlreadyExists(string message = "User already exists!") : base(message)
        {
        }
    }
}
