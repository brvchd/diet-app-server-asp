using System.Collections;
using System.Collections.Generic;

namespace diet_server_api.DTO.Responses.Diet
{
    public class GetDietMealsResponse
    {
        public int IdDay { get; set; }
        public int DayNumber { get; set; }
        public string PatientReport { get; set; }
        public IEnumerable<DayMealTake> Meals { get; set; }
    }
    public class DayMealTake
    {
        public string NameOfMeal { get; set; }
        public string Description { get; set; }
        public string Cooking_URL { get; set; }
        public string Time { get; set; }
        public bool? IsFollowed { get; set; }
        public decimal? Proportion { get; set; }
        public IEnumerable<MealRecipe> Recipes { get; set; }
    }

    public class MealRecipe
    {
        public int IdProduct { get; set; }
        public decimal Amount { get; set; }
        public decimal Size { get; set; }
        public string Name { get; set; }
        public string HomeMeasure { get; set; }
        public decimal HomeMeasureSize { get; set; }
        public string Unit { get; set; }
    }
}

