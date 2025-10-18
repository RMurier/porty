using api.Data;
using api.Data.Models;
using api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public class USerService : IUser
    {
        internal readonly PortyDbContext _ctx;
        public USerService(PortyDbContext ctx)
        {
            _ctx = ctx;
        }
    }
}
