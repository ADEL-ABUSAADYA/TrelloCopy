namespace TrelloCopy.Features.ProjectManage.GetAllProject.Query
{
    public class GetProjectDTo
    {
        public string title { get; set; }

        public string description { get; set; }

        public int NumUSers { get; set; }

        public int NumTask { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
