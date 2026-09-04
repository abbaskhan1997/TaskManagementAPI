using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.Data;
using TaskManagementAPI.Services;

namespace TaskManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
            var userId = int.Parse(User.FindFirst(
                System.Security.Claims.ClaimTypes.NameIdentifier
            )!.Value);

            task.UserId = userId;

            var createdTask = await _taskService.CreateTask(task);

            return Ok(createdTask);
        }


        [HttpGet]
        public async Task<IActionResult> GetTasks ()
        {
            var userId = int.Parse(User.FindFirst(
                System.Security.Claims.ClaimTypes.NameIdentifier
            )!.Value);

            var tasks = await _taskService.GetTasks(userId);

            return Ok(tasks);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById (int id)
        {
            var userId = int.Parse(User.FindFirst(
                System.Security.Claims.ClaimTypes.NameIdentifier
            )!.Value);

            var task = await _taskService.GetTaskById(id);

            if (task == null)
            {
                return NotFound();
            }

            if (task.UserId != userId)
            {
                return Forbid();
            }

            return Ok(task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask (int id, TaskManagementAPI.Models.Task task)
        {
            var userId = int.Parse(User.FindFirst(
                System.Security.Claims.ClaimTypes.NameIdentifier
            )!.Value);

            if (id != task.Id)
            {
                return BadRequest();
            }

            var existingTask = await _taskService.GetTaskById(id);

            if (existingTask == null)
            {
                return NotFound();
            }

            if (existingTask.UserId != userId)
            {
                return Forbid();
            }

            task.UserId = userId;

            var updatedTask = await _taskService.UpdateTask(existingTask,task);

            return Ok(updatedTask);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask (int id)
        {
            var userId = int.Parse(User.FindFirst(
                System.Security.Claims.ClaimTypes.NameIdentifier
            )!.Value);

            var task = await _taskService.GetTaskById(id);

            if (task == null)
            {
                return NotFound();
            }

            if (task.UserId != userId)
            {
                return Forbid();
            }

            await _taskService.DeleteTask(id);

            return NoContent();
        }
    }
}
