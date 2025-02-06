using FluentValidation;
using TrelloCopy.Features.ProjectManage.DeleteProject;

namespace TrelloCopy.Features.TaskManagement.Tasks.GetTaskDetails
{
    public record RequestParame(int id);
    public class RequestParameValidator : AbstractValidator<RequestParame>
    {
        public RequestParameValidator()
        {

        }
    }
}