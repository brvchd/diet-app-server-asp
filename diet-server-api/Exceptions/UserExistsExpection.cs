using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace diet_server_api.Exceptions
{
    public class UserExistsExpection : Exception
    {
        public UserExistsExpection(string message = "User already exists") : base(message)
        {
        }
    }
}
