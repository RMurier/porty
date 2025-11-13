using api.Data.Models;

namespace api.Interfaces
{
    public interface IUser
    {
        public Task<User?> GetUserByEmail(string email);
        public Task<User> AddUser(User user);
        public Task<User> ConfirmUser(User user);
    }
}
