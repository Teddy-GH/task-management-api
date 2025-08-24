using task_management_system.enums;

namespace task_management_system.Dto
{
    public class UserResponseDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
