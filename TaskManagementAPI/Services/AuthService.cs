using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TaskManagementAPI.Data;
using TaskManagementAPI.DTOs;
using TaskManagementAPI.Models;



namespace TaskManagementAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        public AuthService(ApplicationDbContext context,
             IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

//Register
        public async System.Threading.Tasks.Task Register (RegisterRequest request)
        {
            var user = new TaskManagementAPI.Models.User
            {
                Name = request.Name,
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = request.Role
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();
        }

        
//Login
public async Task<string?> Login (LoginRequest request)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
            {
                return null;
            }

            bool passwordValid = BCrypt.Net.BCrypt.Verify(
                request.Password,
                user.Password
            );

            if (!passwordValid)
            {
                return null;
            }

            return GenerateJwtToken(user);
        }



        //Generate JWT Token
        private string GenerateJwtToken (TaskManagementAPI.Models.User user)
        {
            var claims = new[]
            {
        new System.Security.Claims.Claim(
            System.Security.Claims.ClaimTypes.NameIdentifier,
            user.Id.ToString()
        ),

        new System.Security.Claims.Claim(
            System.Security.Claims.ClaimTypes.Email,
            user.Email
        ),

        new System.Security.Claims.Claim(
            System.Security.Claims.ClaimTypes.Role,
            user.Role
        )
    };

            var key = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(
                    _configuration["Jwt:Key"]!
                )
            );

            var credentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(
                key,
                Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256
            );

            var token = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    double.Parse(_configuration["Jwt:ExpiryMinutes"]!)
                ),
                signingCredentials: credentials
            );

            return new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler()
                .WriteToken(token);
        }

    }
}
