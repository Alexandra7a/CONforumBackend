using System.Threading.Tasks;
using PUT_Backend.Models;

namespace PUT_Backend.Repositories
{
    public interface IUserDataRepository
    {
        Task<UserData> GetByUserIdAsync(string userId);
        Task AddUserDataAsync(UserData userData);
        Task UpdateUserDataAsync(string userId, UserData updatedData);
    }
}
