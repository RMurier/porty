using api.Data.Models;
using api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CareerController : ControllerBase
    {
        internal ICareer _career;
        internal IJsonLocalizer L;
        public CareerController(ICareer career, IJsonLocalizer l)
        {
            _career = career;
            L = l;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllCareers()
        {
            try
            {
                List<Career> buisness = await _career.GetAllCareers();
                if(buisness.Count == 0)
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
