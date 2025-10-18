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
        public ProjectController(IProject project)
        {
            _project = project ?? throw new ArgumentNullException(nameof(project));
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
                return BadRequest("Une erreur est survenue. Veuillez réessayer plus tard.");
                
            }
            catch (Exception ex)
            {
                return BadRequest("Une erreur est survenue. Veuillez réessayer plus tard.");
            }
        }
    }
}
