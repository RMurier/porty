using api.Data.Models;
using api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ProjectController : ControllerBase
    {
        internal IProject _project;
        internal IJsonLocalizer L;
        public ProjectController(IProject project, IJsonLocalizer l)
        {
            _project = project ?? throw new ArgumentNullException(nameof(project));
            L = l;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetALlProjects()
        {
            try
            {
                List<Project>? projects = await _project.GetAllProjects();
                if(projects == null || projects.Count == 0)
                {
                    return NoContent();
                }
                else if (projects.Count > 0)
                {
                    return Ok(projects);
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
