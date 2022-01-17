using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using diet_server_api.DTO.Requests.Secretary;

namespace diet_server_api.Services.Interfaces
{
    public interface ISecretaryService
    {
        Task SendEmail(SendEmailRequest request);
    }
}