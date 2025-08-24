using task_management_system.Dto;
using task_management_system.Models;

namespace task_management_system.Mappers
{
    public static class TaskItemMapper
    {
        public static TaskItemResponseDto ToTaskItemDto(this TaskItem taskModel)
        {
            return new TaskItemResponseDto
            {
                Id = taskModel.Id,
                Title = taskModel.Title,
                Description = taskModel.Description,
                Status = taskModel.Status,
                Priority = taskModel.Priority,
                AssigneeId = taskModel.AssigneeId,
                CreatorId = taskModel.CreatorId

            };
        }

        public static TaskItem ToTaskItem(this CreateTaskItemDto taskItemDto)
        {
            return new TaskItem
            {
                Title = taskItemDto.Title,
                Description = taskItemDto.Description,
                Status = taskItemDto.Status,
                Priority = taskItemDto.Priority,
                AssigneeId = taskItemDto.AssigneeId,
                CreatorId = taskItemDto.CreatorId
            };
        }
    }
}
