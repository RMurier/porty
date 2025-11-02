using api.Data.Models;

namespace api.Interfaces
{
    public interface IProject
    {
        public Task<List<Project>> GetAllProjects();
    }
}
