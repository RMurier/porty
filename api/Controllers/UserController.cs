using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        public UserController() { }

        [HttpGet("test")]
        public async Task<IActionResult> test()
        {
            return Ok("test");
        }
        [HttpGet]
        public async Task<IActionResult> test2()
        {
            return Ok("test2");
        }
    }
}
