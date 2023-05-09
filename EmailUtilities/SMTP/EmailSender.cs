
using System.Net;
using System.Net.Mail;

namespace EmailUtilities.SMTP
{
    public sealed class EmailSender
    {
        private SmtpClient _client = new SmtpClient()
        {
            Host = EmailCredentials.Host,
            Port = EmailCredentials.Port,
            EnableSsl = EmailCredentials.EnableSsl,
            Credentials = new NetworkCredential(EmailCredentials.EMailAddress, EmailCredentials.EMailPassword)
        };

        public Task SendEmailAsync(string recipient, string subject, string body)
        {
            try
            {
            return _client.SendMailAsync(
                new MailMessage(from: EmailCredentials.EMailAddress,
                                to: recipient,
                                subject,
                                body));
            }
            catch
            {

                throw new Exception("You must define properties in EmailCredentials.cs");
            }
        }
    }
}