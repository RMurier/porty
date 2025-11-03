using api.Data;
using api.Data.Models;
using api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public class ProjectService : IProject
    {
        internal readonly PortyDbContext _ctx;
        public ProjectService(PortyDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<List<Project>> GetAllProjects()
        {
            if (_ctx.Projects.Any())
            {
                return await _ctx.Projects.ToListAsync();
            }
            return new List<Project>();
        }
    }
}
