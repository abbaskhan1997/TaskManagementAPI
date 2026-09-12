using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.DTOs;
using TaskManagementAPI.Services;

namespace TaskManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController (IAuthService authService)
        {
            _authService = authService;
        }

        // Register request
        [HttpPost("register")]
        public async Task<IActionResult> Register (RegisterRequest request)
        {
            

                await _authService.Register(request);

                return Ok(new
                {
                    message = "User registered successfully"
                });
        }
            
            
        

       
// Login request
[HttpPost("login")]
public async Task<IActionResult> Login (LoginRequest request)
        {
            var token = await _authService.Login(request);

            if (token == null)
            {
                return Unauthorized(new
                {
                    message = "Invalid email or password"
                });
            }

            return Ok(new
            {
                message = "Login successful",
                token = token
            });
      }


    }
}

