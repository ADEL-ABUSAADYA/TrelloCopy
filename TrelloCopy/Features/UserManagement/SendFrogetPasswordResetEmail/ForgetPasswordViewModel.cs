using FluentValidation;
using TrelloCopy.Features.UserManagement.RegisterUser;

namespace TrelloCopy.Features.UserManagement.SendFrogetPasswordResetEmail
{
    public record ForgetPasswordViewModel(string email); 


     public class ForgetPasswordViewModelValidator : AbstractValidator<ForgetPasswordViewModel>
    {
        public ForgetPasswordViewModelValidator ()
        {
         
        }
    }

}