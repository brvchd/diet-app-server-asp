using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace diet_server_api.DTO.Responses.Diet
{
    public class CreateDietResponse
    {
        public int IdDiet { get; set; }
        public string Name { get; set; }
    }
}