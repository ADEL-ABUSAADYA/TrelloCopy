namespace TrelloCopy.Features.TaskManagement.Tasks.GetAllTasks.Query
{
    public record GetTaskDto(string Title,string Description , string User , string Status , string Project,DateTime CreatedDate );
}
