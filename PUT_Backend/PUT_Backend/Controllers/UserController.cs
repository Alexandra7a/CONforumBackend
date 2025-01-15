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

        public UserController(IUserService userService)
        {
            _userService = userService;
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

        [HttpGet("profile/{username}")]
        public async Task<ActionResult<object>> GetUserProfile(string username)
        {
            var (user, userData) = await _userService.GetUserProfileAsync(username);

            if (user == null || userData == null)
                return NotFound("User profile not found.");

            var profile = new
            {
                user.Id,
                user.Username,
                user.Email,
                user.IsAdmin,
                user.Banned,
                userData.LikedPostsIds,
                userData.PostsIds,
                userData.CommentsIds,
                userData.StinksNr
            };

            return Ok(profile);
        }

    }
}
