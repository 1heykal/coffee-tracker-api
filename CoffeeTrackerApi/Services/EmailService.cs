using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Security;
using MailKit.Net.Smtp;
using MimeKit.Text;

namespace CoffeeTrackerApi.Services
{
    public class EmailService
    {
        private readonly EmailSettings _emailSettings;
        public EmailService(IOptions<EmailSettings> emailOptions)
        {
            _emailSettings = emailOptions.Value;
        }

        public void SendEmail(string messageBody)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailSettings.FromName, _emailSettings.FromAddress));
            message.To.Add(new MailboxAddress(_emailSettings.ToName, _emailSettings.ToAddress));

            message.Subject = "Home Compass Email Verification";

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = messageBody;

            message.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            client.Authenticate(_emailSettings.GmailUsername, _emailSettings.GmailPassword);

            client.Send(message);
            client.Disconnect(true);

        }
    }
}
