using TrelloCopy.Common.Helper;

namespace TrelloCopy.Features.TaskManagement.Tasks.GetAllTasks
{
    public class GetFilteredTasksParams : QueryStringParamater
    {
        public Dictionary<string, object> filters { get; set; } = new();
    }
}
