using FluentValidation;
using TrelloCopy.Features.AuthManagement.RegisterUser;

namespace TrelloCopy.Features.AuthManagement.SendFrogetPasswordResetEmail
{
    public record ForgetPasswordViewModel(string email); 


     public class ForgetPasswordViewModelValidator : AbstractValidator<ForgetPasswordViewModel>
    {
        public ForgetPasswordViewModelValidator ()
        {
         
        }
    }

}