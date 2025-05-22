using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Data;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Controllers
{
    [ApiController]
    [Route("tasks")]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TasksController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ POST /tasks → Create a new task
        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TaskCreateDto taskDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var task = new TaskItem
            {
                Title = taskDto.Title,
                Description = taskDto.Description,
                AssignedToUserId = taskDto.AssignedToUserId
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
        }


        // ✅ GET /tasks/{id} → Get task by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task = await _context.Tasks
                .Include(t => t.Comments)
                .Include(t => t.AssignedTo)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (task == null)
                return NotFound();

            return Ok(task);
        }

        // ✅ GET /tasks/user/{userId} → Get tasks assigned to specific user
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetTasksByUser(int userId)
        {
            var tasks = await _context.Tasks
                .Where(t => t.AssignedToUserId == userId)
                .Include(t => t.Comments)
                .ToListAsync();

            return Ok(tasks);
        }
    }
}
