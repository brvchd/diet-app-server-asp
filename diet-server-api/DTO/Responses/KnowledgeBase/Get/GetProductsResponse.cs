using System.Collections.Generic;

namespace diet_server_api.DTO.Responses.KnowledgeBase.Get
{
    public class GetProductsResponse
    {
        public int IdProduct { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public decimal Size { get; set; }
        public string HomeMeasure { get; set; }
        public decimal HomeMeasureSize { get; set; }
        public List<Parameter> Parameters { get; set; }

        public class Parameter
        {
            public string Name { get; set; }
            public string MeasureUnit { get; set; }
            public decimal Amount { get; set; }
        }
    }
}