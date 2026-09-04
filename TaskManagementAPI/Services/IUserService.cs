namespace TaskManagementAPI.Services
{
    public interface IUserService
    {
        Task<List<TaskManagementAPI.Models.User>> GetUsers ();
    }
}
