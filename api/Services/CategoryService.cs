using api.Data;
using api.Data.Models;
using api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public class CategoryService : ICategory
    {
        internal readonly PortyDbContext _ctx;
        public CategoryService(PortyDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            if (_ctx.Categories.Any())
            {
                return await _ctx.Categories.ToListAsync();
            }
            return new List<Category>();
        }
    }
}
