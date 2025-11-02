using api.Data;
using api.Data.Models;
using api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public class UserService : IUser
    {
        internal readonly PortyDbContext _ctx;
        public UserService(PortyDbContext ctx)
        {
            _ctx = ctx;
        }
    }
}
