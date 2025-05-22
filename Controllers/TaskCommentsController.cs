using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Data;
using TaskManagementAPI.Models;

[ApiController]
[Route("api/[controller]")]
public class TaskCommentsController : ControllerBase
{
    private readonly AppDbContext _context;

    public TaskCommentsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateComment(TaskCommentCreateDto dto)
    {
        // Validation is automatically done due to [Required]

        var comment = new TaskComment
        {
            Comment = dto.Comment,
            TaskItemId = dto.TaskItemId,
            UserId = dto.UserId
        };

        _context.TaskComments.Add(comment);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCommentById), new { id = comment.Id }, comment);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCommentById(int id)
    {
        var comment = await _context.TaskComments
            .Include(tc => tc.User)
            .Include(tc => tc.TaskItem)
            .FirstOrDefaultAsync(tc => tc.Id == id);

        if (comment == null)
            return NotFound();

        return Ok(comment);
    }
}
