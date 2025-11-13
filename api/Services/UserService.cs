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
        public async Task<User?> GetUserByEmail(string email)
        {
            return await _ctx.Users.FirstOrDefaultAsync(x => x.Email == email);
        }
        public async Task<User> AddUser(User user)
        {
            _ctx.Users.Add(user);
            await _ctx.SaveChangesAsync();
            return user;
        }
        public async Task<User> ConfirmUser(User user)
        {
            User? updateUser = _ctx.Users.FirstOrDefault(x => x.Id == user.Id);
            updateUser.TokenAccountCreated = null;
            updateUser.IsEmailValidated = true;
            await _ctx.SaveChangesAsync();
            return user;
        }

    }
}
