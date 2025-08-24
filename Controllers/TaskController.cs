using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using task_management_system.Dto;
using task_management_system.Interfaces;
using task_management_system.Mappers;

namespace task_management_system.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskItemRepository _taskItemRepository;
        public TaskController(ITaskItemRepository taskItemRepository)
        {
            _taskItemRepository = taskItemRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTaskItems([FromQuery] TaskStatus? status, [FromQuery] Guid? assignee)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var taskItems = await _taskItemRepository.GetAllTasks();

            if (status.HasValue)
                taskItems = taskItems.Where(t => (TaskStatus)t.Status == status.Value).ToList();

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
            await _taskItemRepository.CreateTaskItem(taskItemModel);
            return CreatedAtAction(nameof(GetTaskItem), new { id = taskItemModel.Id }, taskItemModel.ToTaskItemDto());


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



            return Ok(taskItemModel.ToTaskItemDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteTAskItem([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var taskItemModel = await _taskItemRepository.DeleteTaskItem(id);

            if (taskItemModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
