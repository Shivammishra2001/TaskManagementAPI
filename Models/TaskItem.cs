using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TaskManagementAPI.Models;

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public int AssignedToUserId { get; set; }
    public User AssignedTo { get; set; }

    public ICollection<TaskComment> Comments { get; set; }
}
