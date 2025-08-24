using task_management_system.Dto;

namespace task_management_system.Interfaces
{
    public interface ITasksClientHub
    {
        Task TaskCreated(TaskItemResponseDto task);
        Task TaskUpdated(TaskItemResponseDto task);
        Task TaskDeleted(Guid taskId);
    }
}
