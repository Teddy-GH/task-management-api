using task_management_system.Models;

namespace task_management_system.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(User user);
    }
}
