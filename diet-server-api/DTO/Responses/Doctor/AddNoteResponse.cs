using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace diet_server_api.DTO.Responses.Doctor
{
    public class AddNoteResponse
    {
        public int IdNote { get; set; }
        public string Message { get; set; }
    }
}