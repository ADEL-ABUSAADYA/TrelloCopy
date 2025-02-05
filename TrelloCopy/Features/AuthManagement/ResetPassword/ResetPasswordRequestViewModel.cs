using FluentValidation;
using System.ComponentModel.DataAnnotations;
using TrelloCopy.Features.AuthManagement.RegisterUser;

namespace TrelloCopy.Features.AuthManagement.ResetPassword
{
    public record ResetPasswordRequestViewModel
    {

        public string otp {  get; set; }

        public string Password { get; set; }

        
        public string Email { get; set; }
        
    
        public string ConfirmPassword{ get; set; }

    }


    public class ResetPasswordRequestViewModelValidator : AbstractValidator<ResetPasswordRequestViewModel>
    {
        public ResetPasswordRequestViewModelValidator()
        {
            RuleFor(x => x.otp)
                .NotEmpty().WithMessage("OTP is required.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Please provide a valid email address.")
                .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
                .WithMessage("Please enter a correctly formatted email address.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"\d").WithMessage("Password must contain at least one number.")
                .Matches(@"[\W_]").WithMessage("Password must contain at least one special character (!@#$%^&* etc.).");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm Password is required.")
                .Equal(x => x.Password).WithMessage("Confirm Password must match Password.");
        }
    }
}