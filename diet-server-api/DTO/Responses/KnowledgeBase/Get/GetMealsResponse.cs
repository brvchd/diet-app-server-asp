using System.Collections.Generic;

namespace diet_server_api.DTO.Responses.KnowledgeBase.Get
{
    public class GetMealsResponse
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalRows { get; set; }
        public List<MealRecipe> Meals { get; set; }
        public class MealRecipe
        {
            public int IdMeal { get; set; }
            public string NameOfMeal { get; set; }
            public string Description { get; set; }
            public string CookingURL { get; set; }

            public List<RecipeProduct> Products { get; set; }
            public class RecipeProduct
            {
                public int IdMealRecipe { get; set; }
                public int IdProduct { get; set; }
                public string Name { get; set; }
                public string Unit { get; set; }
                public decimal Size { get; set; }
                public string HomeMeasure { get; set; }
                public decimal HomeMeasureSize { get; set; }
                public decimal Amount { get; set; }
            }

        }

    }
}