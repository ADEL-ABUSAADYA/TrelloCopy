using FluentValidation;
using TrelloCopy.Common.Helper;

namespace TrelloCopy.Features.UserManagement.GetAllUsers;

public class PaginationRequestViewModel() : QueryStringParamater; 

public class PaginationRequestViewModelValidator : AbstractValidator<PaginationRequestViewModel>
{
    public PaginationRequestViewModelValidator()
    {
        // RuleFor(x => x.PageNumber)
        //     .GreaterThanOrEqualTo(1)
        //     .WithMessage("PageNumber must be greater than or equal to 1.");
        //
        // RuleFor(x => x.PageSize)
        //     .GreaterThanOrEqualTo(1)
        //     .LessThanOrEqualTo(100)
        //     .WithMessage("PageSize must be between 1 and 100.");
    }
}