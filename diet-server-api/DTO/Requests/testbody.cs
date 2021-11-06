using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace diet_server_api.DTO.Requests
{
    public class Testbody
    {
        [Required]
        public int A { get; set; }
        [Required]
        public string B { get; set; }
        public string C { get; set; }
        public int? D { get; set; }
    }
}