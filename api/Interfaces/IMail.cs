using api.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Interfaces
{
    public interface IMail
    {
        public Task SendEmail(string to, string subject, string htmlBody, string? textBody = null, CancellationToken ct = default);
        public Task<MailTemplateTranslation?> GetMailTemplate(string nameTemplate, string locale);
    }
}
