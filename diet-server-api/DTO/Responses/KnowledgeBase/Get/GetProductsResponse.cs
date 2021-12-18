using System.Collections.Generic;

namespace diet_server_api.DTO.Responses.KnowledgeBase.Get
{
    public class GetProductsResponse
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalRows { get; set; }
        public List<Product> Products { get; set; }
        public class Product
        {
            public int IdProduct { get; set; }
            public string Name { get; set; }
            public string Unit { get; set; }
            public decimal Size { get; set; }
            public string HomeMeasure { get; set; }
            public decimal HomeMeasureSize { get; set; }
            public List<Parameter> Parameters { get; set; }
        }
        public class Parameter
        {
            public int IdParameter { get; set; }
            public string Name { get; set; }
            public string MeasureUnit { get; set; }
            public decimal Amount { get; set; }
        }

    }
}