using FluentValidation;

namespace TrelloCopy.Features.AuthManagement.ConfirmUserRegistration;

public record ConfirmEmailRequestViewModel(string Email, string Token);

public class ConfirmEmailRequestViewModelValidator : AbstractValidator<ConfirmEmailRequestViewModel>
{
    public ConfirmEmailRequestViewModelValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Please provide a valid email address.");

        RuleFor(x => x.Token)
            .NotEmpty().WithMessage("Token is required.")
            .MinimumLength(6).WithMessage("Token must be at least 6 characters long.");
    }
}