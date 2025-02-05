using TrelloCopy.Features.ProjectManage.GetAllProject.Query;

namespace TrelloCopy.Features.ProjectManage.GetProjectDetailes
{
    public class ProjectDetailsResponseViewModel
    {

        public string title { get; set; }

        public string description { get; set; }

        public int NumUSers { get; set; }

        public int NumTask { get; set; }

        public DateTime CreatedDate { get; set; }



    }
}