using System;
namespace diet_server_api.Helpers.Calculators
{
    public class ModifiedFormulaCalculator
    {
        public static decimal CalculateFormula(decimal height)
        {
            decimal result = height - 100 + (height - 100) * 20 / 100;
            return decimal.Round(result, 2);
        }
    }
}