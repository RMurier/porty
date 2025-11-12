using api.Data.Models;
using api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BuisnessController : ControllerBase
    {
        internal IBuisness _buisness;
        internal IJsonLocalizer L;
        public BuisnessController(IBuisness buisness, IJsonLocalizer l)
        {
            _buisness = buisness;
            L = l;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllBuisnesses()
        {
            try
            {
                List<Buisness>? buisness = await _buisness.GetAllBuisnesses();
                if(buisness == null || buisness.Count == 0)
                {
                    return NoContent();
                }
                else if (buisness.Count > 0)
                {
                    return Ok(buisness);
                }
                return BadRequest(L["ServerError"]);
                
            }
            catch 
            {
                return BadRequest(L["ServerError"]);
            }
        }
    }
}
