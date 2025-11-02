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

        public async Task<List<Buisness>> GetAllBuisnesses()
        {
            if (_ctx.Buisnesses.Any())
            {
                return await _ctx.Buisnesses.ToListAsync();
            }
            return new List<Buisness>();
        }
    }
}
