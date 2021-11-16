using System;

namespace diet_server_api.Exceptions
{
    public class MealsBeforeDietNotFound : Exception
    {
        public MealsBeforeDietNotFound(string message = "Meals before diet are not found") : base(message)
        {
        }
    }
}