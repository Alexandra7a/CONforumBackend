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

            if (user == null || userData == null)
                return NotFound("User profile not found.");

            Console.WriteLine($"PostsIds: {string.Join(", ", userData.PostsIds)}");

            if (userData.PostsIds == null || !userData.PostsIds.Any())
                return NotFound("No posts associated with the user.");

            var userPostsTasks = userData.PostsIds.Select(id => _postService.GetPostByIdAsync(id));
            var userPosts = await Task.WhenAll(userPostsTasks);

            var likedPostsTasks = userData.LikedPostsIds.Select(id => _postService.GetPostByIdAsync(id));
            var likedPosts = await Task.WhenAll(likedPostsTasks);

            var commentsTasks = userData.CommentsIds.Select(id => _commentService.GetCommentByIdAsync(id));
            var comments = await Task.WhenAll(commentsTasks);

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
