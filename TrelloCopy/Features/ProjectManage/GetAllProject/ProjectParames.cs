using FluentValidation;
using TrelloCopy.Common.Helper;
using TrelloCopy.Features.ProjectManage.AddProject;

namespace TrelloCopy.Features.ProjectManage.GetAllProject
{
    public class ProjectParames : QueryStringParamater
    {
        public string? title { get; set; }
    }

    public class ProjectParamesValidator : AbstractValidator<ProjectParames>
    {
        public ProjectParamesValidator()
        {

        }
    }



}