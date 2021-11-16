using System;
using diet_server_api.Exceptions;

namespace diet_server_api.Helpers.Calculators
{
    public class BasicMetabolismCalculator
    {
        public static decimal CalculateBasicMetabolism(string gender, decimal IdealBodyWeight, decimal height, int age)
        {
            gender = gender.Trim();
            if (gender != "Male" || gender != "Female") throw new IncorrectGender();

            decimal result = 0;

            if (gender == "Male")
            {
                result = 66.47m + (13.75m * IdealBodyWeight) + (5 * height) - (6.75m * age);
            }
            else if (gender == "Male")
            {
                result = 665.09m + (9.56m * IdealBodyWeight) + (1.85m * height) - (4.67m * age);
            }
            return decimal.Round(result, 2);
        }
    }
}