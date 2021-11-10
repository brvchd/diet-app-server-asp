using System;

namespace diet_server_api.Exceptions
{
    public class UserIsPending : Exception
    {
        public UserIsPending(string message = "User is still pending!") : base(message)
        {
        }
    }
}