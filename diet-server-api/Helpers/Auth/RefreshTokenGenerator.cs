using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace diet_server_api.Helpers.Auth
{
    public class RefreshTokenGenerator
    {
        public static string GenerateRefreshToken() => Guid.NewGuid().ToString();
    }
}