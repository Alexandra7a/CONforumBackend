using Microsoft.AspNetCore.Mvc;
using PUT_Backend.Models;
using PUT_Backend.Services;
using System.Threading.Tasks;

namespace PUT_Backend.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (loginRequest == null || string.IsNullOrEmpty(loginRequest.Username) || string.IsNullOrEmpty(loginRequest.Password))
            {
                return BadRequest("Username and password are required.");
            }

            var token = await _authService.LoginAsync(loginRequest.Username, loginRequest.Password);

            if (token == null)
            {
                return Unauthorized("Invalid credentials.");
            }

            return Ok(new { token });
        }
    }
}
