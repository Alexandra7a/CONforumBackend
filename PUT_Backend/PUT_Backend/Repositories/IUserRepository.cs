using System.Threading.Tasks;
using PUT_Backend.Models;

namespace PUT_Backend.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsernameAsync(string username);
        Task<User> GetUserByIdAsync(string userId);
        Task<IEnumerable<User>> GetAllUsersAsync();
    }
}
