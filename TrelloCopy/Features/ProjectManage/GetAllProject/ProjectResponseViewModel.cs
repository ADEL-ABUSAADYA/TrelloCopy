using TrelloCopy.Features.ProjectManage.GetAllProject.Query;

namespace TrelloCopy.Features.ProjectManage.GetAllProject
{
    public class ProjectResponseViewModel
    {
        public List<GetProjectDTo> Projects { get; set; }

        public int totalNumber { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }


    }
}