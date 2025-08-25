using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using task_management_system.Dto;
using task_management_system.enums;
using task_management_system.Hubs;
using task_management_system.Interfaces;
using task_management_system.Mappers;

namespace task_management_system.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskItemRepository _taskItemRepository;
        private readonly IHubContext<TasksHub, ITasksClientHub> _hub;
        public TaskController(ITaskItemRepository taskItemRepository, IHubContext<TasksHub, ITasksClientHub> hub)
        {
            _taskItemRepository = taskItemRepository;
            _hub = hub;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTaskItems([FromQuery] Status? status, [FromQuery] Guid? assignee)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var taskItems = await _taskItemRepository.GetAllTasks();

            if (status.HasValue)
                taskItems = taskItems.Where(t =>t.Status == status.Value).ToList();

            if (assignee.HasValue)
            taskItems = taskItems.Where(t => t.AssigneeId == assignee.Value.ToString()).ToList();

            var taskItemDtos = taskItems.OrderByDescending(t => t.UpdatedAt).Select(t => t.ToTaskItemDto());

            return Ok(taskItemDtos);
        }




        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskItem([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var taskItem = await _taskItemRepository.GetTaskById(id);
            if (taskItem == null)
            {
                return NotFound();
            }

            return Ok(taskItem.ToTaskItemDto());


        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateTaskItemDto taskItemDto) 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var taskItemModel = taskItemDto.ToTaskItem();
            var taskCreatedDto = await _taskItemRepository.CreateTaskItem(taskItemModel);

            await _hub.Clients.All.TaskCreated(taskCreatedDto.ToTaskItemDto());

            return CreatedAtAction(nameof(GetTaskItem), new { id = taskItemModel.Id }, taskCreatedDto);


        }



        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateTaskItem([FromRoute] Guid id, [FromBody] UpdateTaskItemDto updateTaskItemDto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var taskItemModel = await _taskItemRepository.UpdateTaskItem(id, updateTaskItemDto);

            if (taskItemModel == null)
            {
                return NotFound();
            }
            await _hub.Clients.All.TaskUpdated(taskItemModel.ToTaskItemDto());



            return Ok(taskItemModel.ToTaskItemDto());
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteTAskItem([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var taskItemModel = await _taskItemRepository.DeleteTaskItem(id);

            if (taskItemModel == null)
            {
                return NotFound();
            }
            await _hub.Clients.All.TaskDeleted(id);

            return NoContent();
        }

    }
}
