using PUT_Backend.Models;

namespace PUT_Backend.Services
{
    public interface IUserService
    {
        Task<User> GetUserByUsernameAsync(string username);
        Task<User> GetUserByIdAsync(string userId);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<(User, UserData)> GetUserProfileAsync(string username);
        Task AddUserDataAsync(UserData userData);
        Task UpdateUserDataAsync(string userId, UserData updatedData);
        Task AddPostToUserAsync(string userId, string postId);
        Task AddCommentToUserAsync(string userId, string commentId);
    }
}