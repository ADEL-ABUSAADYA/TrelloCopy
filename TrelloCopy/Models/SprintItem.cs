using TrelloCopy.Models.Enums;

namespace TrelloCopy.Models;

public class SprintItem : BaseModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public SprintItemStatus Status { get; set; }
    
    public int ProjectID { get; set; }
    public Project Project { get; set; }
    
    //public ICollection<UserSprintItem> UserSprintItems { get; set; }
    public int UserID { get; set; } 
    public User User { get; set; }
}