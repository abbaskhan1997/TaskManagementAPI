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
    }
}
