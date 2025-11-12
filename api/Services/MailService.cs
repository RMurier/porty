using api.Data;
using api.Data.Models;
using api.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace api.Services
{
    public class MailService : IMail
    {
        private readonly IConfiguration _config;
        private readonly PortyDbContext _ctx;
        private readonly IHostEnvironment _env;

        public MailService(IConfiguration config, PortyDbContext ctx, IHostEnvironment env)
        {
            _config = config;
            _ctx = ctx;
            _env = env;
        }

        public async Task SendEmail(string to, string subject, string htmlBody, string? textBody = null, CancellationToken ct = default)
        {
            IConfigurationSection SMTP = _config.GetSection("SMTP");
            var host = SMTP["Host"] ?? "smtp.mail.ovh.net";
            var port = int.TryParse(SMTP["Port"], out var p) ? p : 587;
            var user = SMTP["MailAdress"] ?? throw new InvalidOperationException("SMTP__MailAdress manquant");
            var pass = SMTP["Password"] ?? throw new InvalidOperationException("SMTP__Password manquant");

            var msg = new MimeMessage();
            msg.From.Add(MailboxAddress.Parse(user));
            msg.To.Add(MailboxAddress.Parse(to));
            msg.Subject = subject;
            msg.Body = new BodyBuilder { HtmlBody = htmlBody, TextBody = textBody }.ToMessageBody();

            using var client = new SmtpClient();

            client.CheckCertificateRevocation = !_env.IsDevelopment();
            if (_env.IsDevelopment())
            {
                client.ServerCertificateValidationCallback = (sender, cert, chain, errors) =>
                {
                    if ((errors & SslPolicyErrors.RemoteCertificateNameMismatch) != 0) return false;
                    return true;
                };
            }

            var secure = port == 465 ? SecureSocketOptions.SslOnConnect : SecureSocketOptions.StartTls;

            await client.ConnectAsync(host, port, secure, ct);
            await client.AuthenticateAsync(user, pass, ct);
            await client.SendAsync(msg, ct);
            await client.DisconnectAsync(true, ct);
        }

        public async Task<MailTemplateTranslation?> GetMailTemplate(string nameTemplate, string locale)
        {
            var mailTemplate = _ctx.MailTemplates.FirstOrDefault(x => x.Name.ToLower() == nameTemplate.ToLower());
            if (mailTemplate == null) return null;
            return _ctx.MailTemplatesTranslation.FirstOrDefault(x => x.RefTemplate == mailTemplate.Id && x.Locale.ToLower() == locale.ToLower());
        }
    }
}
