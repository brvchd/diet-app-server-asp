using System;

namespace diet_server_api.Helpers.Calculators
{
    public class AgeCalculator
    {
        public static int CalculateAge(DateTime dob)
        {
            int age = DateTime.Now.AddYears(-dob.Year).Year;
            return age;
        }
    }
}