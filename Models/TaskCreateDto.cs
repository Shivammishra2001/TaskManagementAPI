using System.ComponentModel.DataAnnotations;

public class TaskCreateDto
{
    [Required]
    public string Title { get; set; }

    public string Description { get; set; }

    [Required]
    public int AssignedToUserId { get; set; }
}
