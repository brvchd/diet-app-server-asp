using System;

namespace diet_server_api.Helpers.Auth
{
    public class RefreshTokenGenerator
    {
        public static string GenerateRefreshToken() => Guid.NewGuid().ToString();
    }
}