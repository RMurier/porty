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
        public BuisnessController(IBuisness buisness)
        {
            _buisness = buisness;
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
                return BadRequest("Une erreur est survenue. Veuillez réessayer plus tard.");
                
            }
            catch (Exception ex)
            {
                return BadRequest("Une erreur est survenue. Veuillez réessayer plus tard.");
            }
        }
    }
}
