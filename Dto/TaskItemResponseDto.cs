using task_management_system.enums;
using task_management_system.Models;

namespace task_management_system.Dto
{
    public class TaskItemResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public Status Status { get; set; } = Status.TODO;
        public TaskPriority Priority { get; set; } = TaskPriority.MEDIUM;
        public string? AssigneeId { get; set; }
        public UserResponseDto? Assignee { get; set; }
        public string? CreatorId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}

