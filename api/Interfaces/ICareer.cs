using api.Data.Models;

namespace api.Interfaces
{
    public interface ICareer
    {
        public Task<List<Career>?> GetAllCareers();
    }
}
