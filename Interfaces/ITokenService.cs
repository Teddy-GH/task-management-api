using task_management_system.Models;

namespace task_management_system.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
