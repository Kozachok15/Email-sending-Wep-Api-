using MimeKit;
using MailKit.Net.Smtp;
using MailsWepApi.Services.Settings;
using Microsoft.Extensions.Options;

namespace MailsWepApi.Services
{
    public class EmailService
    {
        private readonly SmtpSettings smtpSettings;

        public EmailService(IOptions<SmtpSettings> smtpSettings)
        {
            this.smtpSettings = smtpSettings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string body)
        {
            using var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("", smtpSettings.Username));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("Plain")
            {
                Text = body
            };

            SmtpSettings settings = new SmtpSettings();

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(smtpSettings.Server, smtpSettings.Port, true);
                await client.AuthenticateAsync(smtpSettings.Username, smtpSettings.Password);

                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
