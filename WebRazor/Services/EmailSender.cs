using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using WebRazor.Contracts;

namespace WebRazor.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;

        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            using (var client = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = _emailSettings.UserName,
                    Password = _emailSettings.Password
                };

                client.Credentials = credential;
                client.Host = _emailSettings.SmtpServer;
                client.Port = _emailSettings.Port;
                client.EnableSsl = true;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_emailSettings.UserName),
                    Subject = subject,
                    Body = htmlMessage,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(email);

                await client.SendMailAsync(mailMessage);
            }
        }
    }

}
