using api.Data.Models;

namespace api.Interfaces
{
    public interface IBuisness
    {
        public Task<List<Buisness>?> GetAllBuisnesses();
    }
}
