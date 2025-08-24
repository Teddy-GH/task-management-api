using Microsoft.AspNetCore.Identity;
using task_management_system.enums;

namespace task_management_system.Models
{
    public class User : IdentityUser
    {
        public Role Role { get; set; } = Role.USER;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
