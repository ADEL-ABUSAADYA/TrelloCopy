using FluentValidation;

namespace TrelloCopy.Features.AuthManagement.ChangePassword;

public record ChangePasswordRequestViewModel(
       string CurrentPassword,
       string NewPassword);
public class ChangePasswordRequestViewModelValidator : AbstractValidator<ChangePasswordRequestViewModel>
{
    public ChangePasswordRequestViewModelValidator()
    {
        RuleFor(x => x.CurrentPassword)
            .NotEmpty().WithMessage("Current password is required.");

        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("New password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"\d").WithMessage("Password must contain at least one number.")
            .Matches(@"[\W_]").WithMessage("Password must contain at least one special character (!@#$%^&* etc.).")
            .NotEqual(x => x.CurrentPassword).WithMessage("New password must be different from the current password.");
    }
}
