using Microsoft.EntityFrameworkCore;
using task_management_system.Data;
using task_management_system.Dto;
using task_management_system.Interfaces;
using task_management_system.Models;

namespace task_management_system.Repository
{
    public class TaskItemRepository : ITaskItemRepository
    {
        private readonly TaskManagementDbContext _taskManagementContext;

        public TaskItemRepository(TaskManagementDbContext taskManagementContext)
        {
            _taskManagementContext = taskManagementContext;
        }
        public async Task<TaskItem> CreateTaskItem(TaskItem taskItem)
        {
            await _taskManagementContext.TaskItems.AddAsync(taskItem);
            await _taskManagementContext.SaveChangesAsync();
            return taskItem;
        }

      

        public async Task<TaskItem?> DeleteTaskItem(Guid id)
        {
            var taskItem = await _taskManagementContext.TaskItems.FirstOrDefaultAsync(t => t.Id == id);

            if (taskItem == null)
            {
                return null;
            }
            _taskManagementContext.TaskItems.Remove(taskItem);
            await _taskManagementContext.SaveChangesAsync();
            return taskItem;
        }

        public async Task<List<TaskItem>> GetAllTasks()
        {
            return await _taskManagementContext.TaskItems.ToListAsync();
        }

        public async Task<TaskItem?> GetTaskById(Guid id)
        {
            return await _taskManagementContext.TaskItems.FindAsync(id);
        }

        public Task<bool> TaskItemExists(Guid id)
        {
            return _taskManagementContext.TaskItems.AnyAsync(t => t.Id == id);
        }

        public async Task<TaskItem?> UpdateTaskItem(Guid id, UpdateTaskItemDto updateTaskItemDto)
        {
            var existingTaskItem = await _taskManagementContext.TaskItems.FirstOrDefaultAsync(t => t.Id == id);
            if (existingTaskItem == null) 
            {
                return null;
            }

            existingTaskItem.Title = updateTaskItemDto.Title;
            existingTaskItem.Description = updateTaskItemDto.Description;
            existingTaskItem.Status = updateTaskItemDto.Status;
            existingTaskItem.Priority = updateTaskItemDto.Priority;
            existingTaskItem.AssigneeId = updateTaskItemDto.AssigneeId;
            existingTaskItem.CreatorId = updateTaskItemDto.UpdatorId;
            //existingTaskItem.CreatedBy = updateTaskItemDto.UpdatedBy;
            existingTaskItem.CreatedAt = updateTaskItemDto.UpdatedAt;

            await _taskManagementContext.SaveChangesAsync();

            return existingTaskItem;
        }
    }
}
