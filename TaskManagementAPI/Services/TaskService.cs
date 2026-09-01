using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Data;

namespace TaskManagementAPI.Services
{
    public class TaskService : ITaskService
    {
        private readonly ApplicationDbContext _context;
        public TaskService (ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<TaskManagementAPI.Models.Task> CreateTask (TaskManagementAPI.Models.Task task)
        {
            _context.Tasks.Add(task);

            await _context.SaveChangesAsync();

            return task;
        }

        public async Task<List<TaskManagementAPI.Models.Task>> GetTasks ()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<TaskManagementAPI.Models.Task> GetTaskById (int id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task<TaskManagementAPI.Models.Task> UpdateTask (TaskManagementAPI.Models.Task task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task DeleteTask (int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }
    }
}
