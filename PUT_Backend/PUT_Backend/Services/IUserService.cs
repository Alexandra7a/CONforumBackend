using PUT_Backend.Models;

namespace PUT_Backend.Services
{
    public interface IUserService
    {
        Task<User> GetUserByUsernameAsync(string username);
        Task<IEnumerable<User>> GetAllUsersAsync();
    }
}