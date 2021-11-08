using System;

namespace diet_server_api.Exceptions
{
    public class RefreshTokenNotFound : Exception
    {
        public RefreshTokenNotFound(string message = "Refresh token not found!") : base(message)
        {
        }
    }
}