using FluentValidation;
using TrelloCopy.Features.ProjectManage.GetAllProject;

namespace TrelloCopy.Features.ProjectManage.GetProjectDetailes
{
    public record ProjectDetailsRequest(int id);

    public class ProjectDetailsRequestValidator : AbstractValidator<ProjectDetailsRequest>
    {
        public ProjectDetailsRequestValidator()
        {

        }
    }


}