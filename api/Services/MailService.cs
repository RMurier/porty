using System.Net.Mail;
using System.Net;
using api.Interfaces;
using System.Collections.Specialized;

namespace api.Services
{
    public class MailService : IMail
    {
        private readonly IConfiguration _config;
        public MailService(IConfiguration config)
        {
            _config = config;
        }
        public async Task SendEmail(string to, string subject, string body)
        {
            IConfigurationSection SMTPConfig = _config.GetSection("SMTP");
            MailAddress fromAddress = new MailAddress(SMTPConfig["MailAdress"], "Romain MURIER");
            MailAddress toAddress = new MailAddress(to);
            string fromPassword = SMTPConfig["Password"];

            SmtpClient smtp = new SmtpClient
            {
                Host = SMTPConfig["Host"],
                Port = SMTPConfig["Port"] == null ? 587 : int.Parse(SMTPConfig["Port"]),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using (MailMessage message = new MailMessage(fromAddress , toAddress)
            {
                IsBodyHtml = true,
                Subject = subject,
                Body = body
            })
            {
                message.Headers.Add("List-Unsubscribe", "<mailto:" + SMTPConfig["MailAdress"] + "?subject=unsubscribe>");
                smtp.Send(message);
            }
        }
    }
}