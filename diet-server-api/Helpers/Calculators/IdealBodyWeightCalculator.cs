using System;
using diet_server_api.Exceptions;

namespace diet_server_api.Helpers.Calculators
{
    public class IdealBodyWeightCalculator
    {
        public static decimal CalculateWeight(string gender, decimal height)
        {
            decimal result = 0;

            if (gender == "Female")
            {
                result = height - 100 - (height - 100) * 10 / 100;
            }
            else if (gender == "Male")
            {
                result = height - 100 - (height - 100) * 5 / 100;
            }
            return decimal.Round(result, 2);
        }
    }
}