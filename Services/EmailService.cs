using System.Net.Mail;
using System.Net;
using iLearn.Services.Interfaces;

namespace iLearn.Services
{
    public class EmailService : IEmailService
    {
        private IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public void SendEmail(string toEmail, string subject, string body)
        {
            try
            {
                var gmailSmtpDetails = _config.GetSection("GmailSmtp");
                var fromEmail = gmailSmtpDetails["FromEmail"];
                var passcode = gmailSmtpDetails["Passcode"];
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(fromEmail);
                mail.To.Add(new MailAddress(toEmail));
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = Convert.ToInt32(gmailSmtpDetails["Port"]),
                    Credentials = new NetworkCredential(fromEmail, passcode),
                    EnableSsl = true
                };
                smtpClient.Send(mail);
            }
            catch (Exception ex)
            {
                throw new Exception("Email not sent", ex);
            }

        }
    }
}
