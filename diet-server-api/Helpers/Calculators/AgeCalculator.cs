using System;

namespace diet_server_api.Helpers.Calculators
{
    public class AgeCalculator
    {
        public static int CalculateAge(DateTime dob)
        {
            int age = DateTime.Today.Year - dob.Year;
            if (dob.Date > DateTime.Today.AddYears(-age)) age--; 
            return age;
        }
    }
}