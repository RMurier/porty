using api.Data.Models;

namespace api.Interfaces
{
    public interface ICategory
    {
        public Task<List<Category>> GetAllCategories();
    }
}
