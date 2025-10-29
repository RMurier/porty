using api.Data.Models;
using api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        internal ICategory _category;
        public CategoryController(ICategory category)
        {
            _category = category ?? throw new ArgumentNullException(nameof(category));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllCareers()
        {
            try
            {
                List<Category>? categories = await _category.GetAllCategories();
                if(categories == null ||categories.Count == 0)
                {
                    return NoContent();
                }
                else if (categories.Count > 0)
                {
                    return Ok(categories);
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
