using System.ComponentModel.DataAnnotations;

public class TaskCommentCreateDto
{
    [Required]
    public string Comment { get; set; }

    [Required]
    public int TaskItemId { get; set; }

    [Required]
    public int UserId { get; set; }
}
