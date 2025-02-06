using FluentValidation;
using TrelloCopy.Data;
using TrelloCopy.Features.AuthManagement.RegisterUser;
using TrelloCopy.Features.AuthManagement.RegisterUser.Commands;

namespace TrelloCopy.Features.ProjectManage.AddProject
{
    public class RequestAddProjectModel
    {
        public string Title { get; set; }
        public string Descrbition { get; set; }
    }

    public class RequestAddProjectModelValidator : AbstractValidator<RequestAddProjectModel>
    {
        private readonly Context _context;

        public RequestAddProjectModelValidator(Context context)
        {

            _context = context;

      
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title is required")
                .Must(BeUniqueTitle);

            RuleFor(x => x.Descrbition)
             .NotEmpty()
             .WithMessage("Description is required");
        }
        private bool BeUniqueTitle(string title)
        {
            return _context.Tasks.All(x => x.Title != title);
        }

    }
}