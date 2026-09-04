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


        public async Task<List<TaskManagementAPI.Models.Task>> GetTasks (int userId)
        {
            return await _context.Tasks
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }

        public async Task<TaskManagementAPI.Models.Task> GetTaskById (int id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task<TaskManagementAPI.Models.Task> UpdateTask (
    TaskManagementAPI.Models.Task existingTask,
    TaskManagementAPI.Models.Task task)
        {
            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.Status = task.Status;
            existingTask.Priority = task.Priority;
            existingTask.DueDate = task.DueDate;

            await _context.SaveChangesAsync();

            return existingTask;
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
