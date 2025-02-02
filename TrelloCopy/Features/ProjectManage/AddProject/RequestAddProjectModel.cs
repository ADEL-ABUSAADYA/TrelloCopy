using FluentValidation;
using TrelloCopy.Features.AuthManagement.RegisterUser;
using TrelloCopy.Features.AuthManagement.RegisterUser.Commands;

namespace TrelloCopy.Features.ProjectManage.AddProject
{
    public class RequestAddProjectModel
    {
        public string Title { get;  set; }
        public string Descrbition { get;  set; }
    }

    public class RequestAddProjectModelValidator : AbstractValidator<RequestAddProjectModel>
    {
        public RequestAddProjectModelValidator()
        {
  
        }
    }
}