using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PUT_Backend.Models;
using PUT_Backend.Services;
using PUT_Backend.Repositories;

namespace PUT_Backend.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;

        public UserController(IUserService userService, IPostService postService, ICommentService commentService)
        {
            _userService = userService;
            _postService = postService;
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<User>> GetUserByUsername(string username)
        {
            var user = await _userService.GetUserByUsernameAsync(username);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            return Ok(user);
        }

        [HttpGet("id/{userId}")]
        public async Task<ActionResult<User>> GetUserById(string userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            return Ok(user);
        }

        [HttpGet("profile/{userId}")]
        public async Task<ActionResult<object>> GetUserProfile(string userId)
        {
            var (user, userData) = await _userService.GetUserProfileAsync(userId);

            if (user == null)
                return NotFound("User not found.");

            if (userData == null)
            {
                userData = new UserData
                {
                    UserId = userId,
                    PostsIds = new List<string>(),
                    LikedPostsIds = new List<string>(),
                    CommentsIds = new List<string>(),
                    StinksNr = 0
                };

                await _userService.AddUserDataAsync(userData);
            }

            Console.WriteLine($"PostsIds: {string.Join(", ", userData.PostsIds)}");

            var userPosts = Array.Empty<object>();
            var likedPosts = Array.Empty<object>();
            var comments = Array.Empty<object>();

            if (userData.PostsIds != null && userData.PostsIds.Any())
            {
                var userPostsTasks = userData.PostsIds.Select(id => _postService.GetPostByIdAsync(id));
                var allPosts = await Task.WhenAll(userPostsTasks);

                userPosts = allPosts.Where(post => post != null && !post.Anonim).ToArray();
            }

            if (userData.LikedPostsIds != null && userData.LikedPostsIds.Any())
            {
                var likedPostsTasks = userData.LikedPostsIds.Select(id => _postService.GetPostByIdAsync(id));
                likedPosts = await Task.WhenAll(likedPostsTasks);
            }

            if (userData.CommentsIds != null && userData.CommentsIds.Any())
            {
                var commentsTasks = userData.CommentsIds.Select(id => _commentService.GetCommentByIdAsync(id));
                comments = await Task.WhenAll(commentsTasks);
            }

            var profile = new
            {
                user.Username,
                user.Email,
                user.IsAdmin,
                user.Banned,
                Posts = userPosts,
                LikedPosts = likedPosts,
                Comments = comments,
                StinksNr = userData.StinksNr
            };

            return Ok(profile);
        }


    }
}
