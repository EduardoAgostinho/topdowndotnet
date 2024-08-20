using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.Models;
using TaskManagementAPI.Services;
using TasksModel = TaskManagementAPI.Models.TasksModel;

namespace TaskManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TasksModel>>> GetTasks()
        {
            var tasks = await _taskService.GetTasksAsync();
            return Ok(tasks);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<TasksModel>> GetTask(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<TasksModel>> CreateTask(TasksModel task)
        {
            await _taskService.CreateTaskAsync(task);
            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, TasksModel task)
        {
            if (id != task.Id)
            {
                return BadRequest();
            }

            await _taskService.UpdateTaskAsync(task);
            return Accepted(task);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            await _taskService.DeleteTaskAsync(id);
            return NoContent();
        }
    }
}
