using System.ComponentModel.DataAnnotations.Schema;
using task_management_system.enums;

namespace task_management_system.Models
{
    public class TaskItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string? Description { get; set; }

        [Column(TypeName = "text")]
        public Status Status { get; set; } = Status.TODO;

        [Column(TypeName = "text")]
        public TaskPriority Priority { get; set; } = TaskPriority.MEDIUM;
        public string? AssigneeId { get; set; }
        public User? Assignee { get; set; }
        public string? CreatorId { get; set; }
        public User CreatedBy { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
