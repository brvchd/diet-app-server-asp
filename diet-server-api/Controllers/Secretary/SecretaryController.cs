using System.Threading.Tasks;
using diet_server_api.DTO.Requests.Secretary;
using diet_server_api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace diet_server_api.Controllers.Secretary
{
    [ApiController]
    [Route("api/secretary")]
    public class SecretaryController : Controller
    {
        private readonly ISecretaryService _secService;
        public SecretaryController(ISecretaryService secService)
        {
            _secService = secService;
        }

        [HttpPost]
        [Authorize(Roles = "SECRETARY")]
        public async Task<IActionResult> SendEmail(SendEmailRequest request)
        {
            await _secService.SendEmail(request);
            return Ok("Email sent");
        }
        
    }
}