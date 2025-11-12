using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IJsonLocalizer L;

        public UserController(IJsonLocalizer l)
        {
            L = l;
        }
    }
}
