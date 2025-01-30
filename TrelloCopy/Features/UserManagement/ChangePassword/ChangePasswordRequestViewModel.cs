using FluentValidation;

namespace TrelloCopy.Features.UserManagement.ChangePassword;

public record ChangePasswordRequestViewModel(
       int UserId,
       string CurrentPassword,
       string NewPassword);


public class ChangePasswordRequestViewModelValidator : AbstractValidator<ChangePasswordRequestViewModel>
{
    public ChangePasswordRequestViewModelValidator()
    {
        RuleFor(x => x.NewPassword).NotEmpty().MinimumLength(6);
        RuleFor(x => x.CurrentPassword).NotEmpty();
    }
}
