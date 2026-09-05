namespace TaskManagementAPI.Services
{
    public interface IUserService
    {
        Task<List<TaskManagementAPI.Models.User>> GetUsers ();

        Task<TaskManagementAPI.Models.User?> GetUserById (int id);

        Task<TaskManagementAPI.Models.User?> UpdateUser (int id, TaskManagementAPI.DTOs.UpdateUserRequest user);

        Task DeleteUser (int id);
    }
}
