using api.Data;
using api.Data.Models;
using api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public class CareerService : ICareer
    {
        internal readonly PortyDbContext _ctx;
        public CareerService(PortyDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<List<Career>?> GetAllBuisnesses()
        {
            return await _ctx.Careers.ToListAsync();
        }
    }
}
