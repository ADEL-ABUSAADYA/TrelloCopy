using MediatR;
using TrelloCopy.Common.Views;
using TrelloCopy.Common;
using TrelloCopy.Common.Data.Enums;
using Microsoft.EntityFrameworkCore;
using TrelloCopy.Common.CommonQuary;
using TrelloCopy.Models;


namespace TrelloCopy.Features.UserManagement.ForgetPassword.Queries
{
    public record ForgetPasswordQuary(string email) : IRequest<RequestResult<bool>>;


    public class ResetPsswordQueryHandler : BaseRequestHandler<ForgetPasswordQuary, RequestResult<bool>, User>
    {
        public ResetPsswordQueryHandler(BaseRequestHandlerParameters<User> parameters) : base(parameters)
        {
        }

        public override async Task<RequestResult<bool>> Handle(ForgetPasswordQuary request, CancellationToken cancellationToken)
        {
            var userExist =await _repository.Get(c=> c.Email == request.email).FirstOrDefaultAsync();

            if (userExist == null) return RequestResult<bool>.Failure(ErrorCode.UserNotFound, "this user not found in database");


            var token =Guid.NewGuid().ToString().Substring(0, 6);
            ;

            var user = new User
            {
                ID = userExist.ID,

            };

            await _repository.SaveIncludeAsync(user, nameof(user.Password)); 
             
             await _repository.SaveChangesAsync();

            var confirmationLink = $"this for reset password {userExist.Email}& token={token}";

            var emailSent = _mediator.Send(new SendEamilQuary(request.email, "this code for reset password", confirmationLink));  
            if (!emailSent.IsCompletedSuccessfully)
                return RequestResult<bool>.Failure(ErrorCode.EmailNotSent);

            return RequestResult<bool>.Success(true);
        }
    }


}
