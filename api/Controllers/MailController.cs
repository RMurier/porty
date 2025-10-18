using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class MailController : ControllerBase
    {
        private readonly IMail _mail;
        public MailController(IMail mail)
        {
            _mail = mail;
        }
        [HttpPost("SendMail")]
        public async Task<IActionResult> SendMail([FromBody] SendMailIn model)
        {
            if (ModelState.IsValid)
            {
                await _mail.SendEmail(model.To, model.Subject, model.Body);
                return NoContent();
            }
            else
            {
                return BadRequest("Une erreur est survenue lors de l'envoi du mail.");
            }
        }
    }
}