using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace diet_server_api.DTO.Requests
{
    public class testbody
    {
        [Required]
        public int a { get; set; }
        [Required]
        public string b { get; set; }
        public string c { get; set; }
        public int? d { get; set; }
    }
}