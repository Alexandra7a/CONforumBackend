﻿using PUT_Backend.Models;
using PUT_Backend.Repositories;

namespace PUT_Backend.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserDataRepository _userDataRepository;

        public UserService(IUserRepository userRepository, IUserDataRepository userDataRepository)
        {
            _userRepository = userRepository;
            _userDataRepository = userDataRepository;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _userRepository.GetUserByUsernameAsync(username);
        }

        public async Task<User> GetUserByIdAsync(string userId)
        {
            return await _userRepository.GetUserByIdAsync(userId);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<(User, UserData)> GetUserProfileAsync(string userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null) return (null, null);

            var userData = await _userDataRepository.GetByUserIdAsync(user.Id);

            return (user, userData);
        }

        public async Task AddUserDataAsync(UserData userData)
        {
            await _userDataRepository.AddUserDataAsync(userData);
        }

        public async Task UpdateUserDataAsync(string userId, UserData updatedData)
        {
            await _userDataRepository.UpdateUserDataAsync(userId, updatedData);
        }

        public async Task AddPostToUserAsync(string userId, string postId)
        {
            var userData = await _userDataRepository.GetByUserIdAsync(userId);

            if (userData == null)
            {
                throw new Exception($"User data for user ID {userId} not found.");
            }

            if (userData.PostsIds == null)
            {
                userData.PostsIds = new List<string>();
            }

            if (!userData.PostsIds.Contains(postId))
            {
                userData.PostsIds.Add(postId);
                await _userDataRepository.UpdateUserDataAsync(userId, userData);
            }
        }

        public async Task AddCommentToUserAsync(string userId, string commentId)
        {
            var userData = await _userDataRepository.GetByUserIdAsync(userId);

            if (userData == null)
            {
                userData = new UserData
                {
                    UserId = userId,
                    CommentsIds = new List<string> { commentId },
                    PostsIds = new List<string>(),
                    LikedPostsIds = new List<string>(),
                    StinksNr = 0
                };

                await _userDataRepository.AddUserDataAsync(userData);
            }
            else
            {
                if (!userData.CommentsIds.Contains(commentId))
                {
                    userData.CommentsIds.Add(commentId);
                    await _userDataRepository.UpdateUserDataAsync(userId, userData);
                }
            }
        }
    }
}
