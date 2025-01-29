using MediatR;
using TrelloCopy.Common.Views;
using TrelloCopy.Common;
using TrelloCopy.Features.UserManagement.RegisterUser.Queries;
using System.Reflection.Metadata.Ecma335;
using TrelloCopy.Common.Data.Enums;
using Microsoft.EntityFrameworkCore;
using TrelloCopy.Models;
using TrelloCopy.Features.Common.CommonQuary;

namespace TrelloCopy.Features.UserManagement.ForgetPassword.Queries
{
    public record ForgetPasswordQuary(string email) : IRequest<RequestResult<bool>>;


    public class ResetPsswordQueryHandler : UserBaseRequestHandler<ForgetPasswordQuary, RequestResult<bool>>
    {
        public ResetPsswordQueryHandler(UserBaseRequestHandlerParameters parameters) : base(parameters)
        {
        }

        public override async Task<RequestResult<bool>> Handle(ForgetPasswordQuary request, CancellationToken cancellationToken)
        {
            var userExist =await _userRepository.Get(c=> c.Email == request.email).FirstOrDefaultAsync();

            if (userExist == null) return RequestResult<bool>.Failure(ErrorCode.UserNotFound, "this user not found in database");


            var token =Guid.NewGuid().ToString().Substring(0, 6);
            ;

            var userr = new User
            {
                ID = userExist.ID,
                ResetPassword = token,
                ResetPasswowrdConfirnation = DateTime.UtcNow.AddMinutes(15)
            };

            await _userRepository.SaveIncludeAsync(userr, nameof(userr.ResetPassword), nameof(userr.ResetPasswowrdConfirnation)); 
             
             await _userRepository.SaveChangesAsync();

            var confirmationLink = $"this for reset password {userExist.Email}& token={token}";

            var emailSent = _mediator.Send(new SendEamilQuary(request.email, "this code for reset password", confirmationLink));  
            if (!emailSent.IsCompletedSuccessfully)
                return RequestResult<bool>.Failure(ErrorCode.EmailNotSent);

            return RequestResult<bool>.Success(true);
        }
    }


}
