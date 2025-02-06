using TrelloCopy.Features.TaskManagement.Tasks.GetAllTasks.Query;

namespace TrelloCopy.Features.Common.TaskBoard
{
    public class TaskBoardResponseModel
    {

        public List<string> ToDO { get; set; }

        public List<string> InProgress { get; set; }

        public List<string> Done { get; set; }



    }
}
