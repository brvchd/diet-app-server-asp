using System;
using System.Security.Cryptography;

namespace diet_server_api.Helpers
{
    public class SaltGenerator
    {
        public static string GenerateSalt()
        {
            var rng = RandomNumberGenerator.Create();
            var saltBytes = new byte[16];
            rng.GetBytes(saltBytes);
            var saltText = Convert.ToBase64String(saltBytes);
            return saltText;
        }
    }
}