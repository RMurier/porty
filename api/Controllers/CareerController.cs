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
        public CareerController(ICareer career)
        {
            _career = career;
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
                return BadRequest("Une erreur est survenue. Veuillez réessayer plus tard.");
                
            }
            catch
            {
                return BadRequest("Une erreur est survenue. Veuillez réessayer plus tard.");
            }
        }
    }
}
