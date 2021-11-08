using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace diet_server_api.Exceptions
{
    public class RefreshTokenExpired : Exception
    {
        public RefreshTokenExpired(string message = "Refresh token expired!") : base(message)
        {
        }
    }
}