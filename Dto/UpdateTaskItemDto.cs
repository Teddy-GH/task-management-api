using System.ComponentModel.DataAnnotations;
using task_management_system.enums;
using task_management_system.Models;

namespace task_management_system.Dto
{
    public class UpdateTaskItemDto
    {
        [Required]
        public string Title { get; set; } = default!;

        public string? Description { get; set; }
        public Status Status { get; set; } = Status.TODO;
        public TaskPriority Priority { get; set; } = TaskPriority.MEDIUM;
        public string? AssigneeId { get; set; }
        public string? UpdatorId { get; set; }
        //public User UpdatedBy { get; set; } = default!;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
