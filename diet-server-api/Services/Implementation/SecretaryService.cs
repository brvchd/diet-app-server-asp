

using System.Threading.Tasks;
using diet_server_api.DTO.Requests.Secretary;
using diet_server_api.Services.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace diet_server_api.Services.Implementation
{
    public class SecretaryService : ISecretaryService
    {
        private readonly IConfiguration _config;

        public SecretaryService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmail(SendEmailRequest request)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Admin Site", "dietappeu@gmail.com"));
            emailMessage.To.Add(new MailboxAddress(request.FullName, request.Email));
            emailMessage.Subject = "Temporary credentials";
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = "hacked"
            };
            using var client = new SmtpClient(); 
            await client.ConnectAsync("smtp.gmail.com",587);
            await client.AuthenticateAsync(_config["Email"], _config["EmailPassword"]);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }
} 