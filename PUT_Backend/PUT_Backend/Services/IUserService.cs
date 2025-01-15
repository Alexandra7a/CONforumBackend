using PUT_Backend.Models;

namespace PUT_Backend.Services
{
    public interface IUserService
    {
        Task<User> GetUserByUsernameAsync(string username);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<(User, UserData)> GetUserProfileAsync(string username);
        Task AddUserDataAsync(UserData userData);
        Task UpdateUserDataAsync(string userId, UserData updatedData);
    }
}