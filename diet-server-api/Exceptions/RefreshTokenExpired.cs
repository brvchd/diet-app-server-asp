using System;

namespace diet_server_api.Exceptions
{
    public class RefreshTokenExpired : Exception
    {
        public RefreshTokenExpired(string message = "Refresh token expired!") : base(message)
        {
        }
    }
}