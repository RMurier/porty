using api.Data;
using api.Data.Models;
using api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public class BuisnessService : IBuisness
    {
        internal readonly PortyDbContext _ctx;
        public BuisnessService(PortyDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<List<Buisness>?> GetAllBuisnesses()
        {
            return await _ctx.Buisnesses.ToListAsync();
        }
    }
}
