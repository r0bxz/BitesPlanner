using BitesPlanner.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace BitesPlanner.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        private readonly JwtService _jwtService;

        public AuthController(UserRepository userRepository, JwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userRepository.GetUserByNameAsync(request.Username);
            if (user == null)
                return Unauthorized("Invalid credentials");

            if (!string.Equals(user.Role, request.Role, StringComparison.OrdinalIgnoreCase))
                return Unauthorized("Invalid role for this user");

            var token = _jwtService.GenerateToken(user);
            return Ok(new { token });
        }

    }
}
