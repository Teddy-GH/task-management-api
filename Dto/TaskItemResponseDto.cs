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
        public Guid? AssigneeId { get; set; }
        public User? Assignee { get; set; }
        public Guid CreatorId { get; set; }
        public User CreatedBy { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}

