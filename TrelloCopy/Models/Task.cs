
using TrelloCopy.Models.Enums;

namespace TrelloCopy.Models;

public class TaskEntity : BaseModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public TaskStatu Status { get; set; }
    public int ProjectId { get; set; }
    public Project Project { get; set; }   
}

