using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagementAPI.Data;
using TaskManagementAPI.DTOs;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController (ApplicationDbContext context,
            IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // Register request
        [HttpPost("register")]
        public async Task<IActionResult> Register (User user)
        {
            try
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    message = "User registered successfully"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred",
                    error = ex.Message
                });
            }
        }

        // login Request
        [HttpPost("login")]
        public async Task<IActionResult> Login (LoginRequest request)
        {
            try
            {
                var user = await _context.Users
             .FirstOrDefaultAsync(u => u.Email == request.Email);

                if (user == null)
                {
                    return Unauthorized(new
                    {
                        message = "Invalid email or password"
                    });
                }

                bool passwordValid = BCrypt.Net.BCrypt.Verify(
                 request.Password,
                  user.Password
                     );

                if (!passwordValid)
                {
                    return Unauthorized(new
                    {
                        message = "Invalid email or password"
                    });
                }

                var claims = new[]
{
    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
    new Claim(ClaimTypes.Email, user.Email),
    new Claim(ClaimTypes.Role, user.Role)
};

                var key = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)
                );

                var credentials = new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256
                );

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(
                        double.Parse(_configuration["Jwt:ExpiryMinutes"]!)
                    ),
                    signingCredentials: credentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(new
                {
                    message = "Login successful",
                    token = tokenString
                });

                
            }


            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred",
                    error = ex.Message
                });
            }
        }
    }
}
