using task_management_system.Dto;
using task_management_system.Models;

namespace task_management_system.Interfaces
{
    public interface ITaskItemRepository
    {
        Task<List<TaskItem>> GetAllTasks();
        Task<TaskItem?> GetTaskById(Guid id);

        Task<TaskItem> CreateTaskItem(TaskItem taskItem);

        Task<TaskItem?> UpdateTaskItem(Guid id, UpdateTaskItemDto updateTaskItemDto );
        Task<TaskItem?> DeleteTaskItem(Guid id);

        Task<bool> TaskItemExists(Guid id);
    }
}
