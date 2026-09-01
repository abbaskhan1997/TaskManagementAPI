using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.Data;
using TaskManagementAPI.Services;

namespace TaskManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        public TaskController (ITaskService taskService)
        {
            _taskService = taskService ;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask (TaskManagementAPI.Models.Task task)
        {
            var createdTask = await _taskService.CreateTask(task);

            return Ok(createdTask);
        }

        [HttpGet]
        public async Task<IActionResult> GetTasks ()
        {
            var tasks = await _taskService.GetTasks();
            return Ok(tasks);
        }

       

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById (int id)
        {
            var task = await _taskService.GetTaskById(id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask (int id, TaskManagementAPI.Models.Task task)
        {
            if (id != task.Id)
            {
                return BadRequest();
            }

            var updatedTask = await _taskService.UpdateTask(task);

            return Ok(updatedTask);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask (int id)
        {
            await _taskService.DeleteTask(id);
            return NoContent();
        }
    }
}
