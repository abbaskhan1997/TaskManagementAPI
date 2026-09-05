using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Data;

namespace TaskManagementAPI.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

       public async Task<List<TaskManagementAPI.Models.User>> GetUsers()
        {
            return await _context.Users.ToListAsync();

           
        }

        public async Task<TaskManagementAPI.Models.User?> GetUserById (int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<TaskManagementAPI.Models.User?> UpdateUser (int id, TaskManagementAPI.DTOs.UpdateUserRequest user)
        {
            var existingUser = await _context.Users.FindAsync(id);

            if (existingUser == null)
            {
                return null;
            }

            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.Role = user.Role;

            await _context.SaveChangesAsync();

            return existingUser;
        }

        public async Task DeleteUser (int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user != null)
            {
                _context.Users.Remove(user);

                await _context.SaveChangesAsync();
            }
        }
    }
}
