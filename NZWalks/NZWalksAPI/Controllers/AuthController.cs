using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly ITokenHandler tokenHandler;

        public AuthController(IUserRepository userRepository, ITokenHandler tokenHandler)
        {
            this.userRepository = userRepository;
            this.tokenHandler = tokenHandler;
        }

        [HttpPost]
        [Route("login")]
         public async Task<IActionResult> LoginAsync(Models.DTO.LoginRequest loginRequest)
        {

            // Validate incoming request
            // created fluent validator for the same

            // Check if user is authenticated
           var user = await userRepository.AuthenticateAsync(
               loginRequest.Username,loginRequest.Password);

            if (user != null)
            {
                // Generate Jwt token
                var token = await tokenHandler.CreateTokenAsync(user);
                return Ok(token);

            }

            return BadRequest("Username or Password is invalid.");

        }
    }
}
