namespace TaskManagementAPI.Services
{
    public interface ITaskService
    {
        Task<TaskManagementAPI.Models.Task> CreateTask (TaskManagementAPI.Models.Task task);

        Task<List<TaskManagementAPI.Models.Task>> GetTasks ();

        Task<TaskManagementAPI.Models.Task?> GetTaskById (int id);

        Task<TaskManagementAPI.Models.Task> UpdateTask (TaskManagementAPI.Models.Task task);

        Task DeleteTask (int id);

    }
}
