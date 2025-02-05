using TrelloCopy.Features.TaskManagement.Tasks.GetAllTasks.Query;

namespace TrelloCopy.Features.TaskManagement.Tasks.GetAllTasks
{
    public class GetAllTasksResponseViewModel
    {
        public ICollection<GetTaskDto> Tasks { get; set; }
        public int TotalNumber { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

    }
}
