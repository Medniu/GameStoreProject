using BLL.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using BLL.Settings;

namespace BLL.Services
{
    public class EmailService : IEmailService
    {
        private readonly IOptions<EmailSettings> config;

        public EmailService(IOptions<EmailSettings> config)
        {
            this.config = config;
        }
       
        public async Task SendEmailAsync(string email, string subject, string message)
        {    
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("My mail", this.config.Value.Email));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };         

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(this.config.Value.Host, this.config.Value.SmptPort, this.config.Value.UseSSL);
                await client.AuthenticateAsync(this.config.Value.Email, this.config.Value.Password);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }           
        }
    }
}
