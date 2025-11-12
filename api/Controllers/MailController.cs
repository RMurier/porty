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
        private readonly IJsonLocalizer L;
        public MailController(IMail mail, IJsonLocalizer l)
        {
            _mail = mail;
            L = l;
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
                return BadRequest(L["ServerError"]);
            }
        }
    }
}