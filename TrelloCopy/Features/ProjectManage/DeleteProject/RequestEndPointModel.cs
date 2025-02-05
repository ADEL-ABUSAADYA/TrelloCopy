using FluentValidation;
using TrelloCopy.Features.ProjectManage.AddProject;

namespace TrelloCopy.Features.ProjectManage.DeleteProject
{
    public record RequestEndPointModel(int id);
    public class RequestEndPointModelValidator : AbstractValidator<RequestEndPointModel>
    {
        public RequestEndPointModelValidator()
        {

        }
    }
}