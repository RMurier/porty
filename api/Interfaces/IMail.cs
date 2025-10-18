using Microsoft.AspNetCore.Mvc;

namespace api.Interfaces
{
    public interface IMail
    {
        public Task SendEmail(string to, string subject, string body);

    }
}
