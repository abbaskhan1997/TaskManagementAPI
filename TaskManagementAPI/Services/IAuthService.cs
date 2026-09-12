using TaskManagementAPI.Models;
using TaskManagementAPI.DTOs;
namespace TaskManagementAPI.Services
{
    public interface IAuthService
    {
        System.Threading.Tasks.Task Register (RegisterRequest request);

        Task<string?> Login (LoginRequest request);
    }
}
