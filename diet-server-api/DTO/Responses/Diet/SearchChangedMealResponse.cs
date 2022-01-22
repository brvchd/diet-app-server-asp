using System.Collections.Generic;

namespace diet_server_api.DTO.Responses.KnowledgeBase.Search
{
    public class SearchChangedMealResponse
    {
        public int IdMeal { get; set; }
        public decimal Proportion { get; set; }
        public string NameOfMeal { get; set; }
        public string Description { get; set; }
        public string CookingURL { get; set; }

        public List<RecipeProduct> Recipes { get; set; }

        public class RecipeProduct
        {
            public int IdMealRecipe { get; set; }
            public int IdProduct { get; set; }
            public string Name { get; set; }
            public string Unit { get; set; }
            // public decimal? CalculatedProductSize { get; set; }
            public string HomeMeasure { get; set; }
            public decimal HomeMeasureSize { get; set; }
            public decimal CalculatedRecipeAmount { get; set; }
            public List<ProductParam> Params { get; set; }
        }
        public class ProductParam
        {
            public decimal CalculatedParamSize { get; set; }
            public string ParamName { get; set; }
            public string ParamMeasureUnit { get; set; }
        }

    }
}

