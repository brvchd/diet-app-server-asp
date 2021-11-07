using System;
using System.Security.Cryptography;
using System.Text;

namespace diet_server_api.Helpers
{
    public class PasswordGenerator
    {
        public static string GeneratePassword(string password, string salt)
        {
            var sha = SHA256.Create();
            var saltedPassword = password + salt;
            return Convert.ToBase64String(sha.ComputeHash(Encoding.Unicode.GetBytes(saltedPassword)));
        }
    }
}