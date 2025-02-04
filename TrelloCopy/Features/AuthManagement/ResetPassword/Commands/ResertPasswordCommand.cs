using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using TrelloCopy.Common.BaseHandlers;
using TrelloCopy.Common.Data.Enums;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.AuthManagement.ResetPassword.Queries;
using TrelloCopy.Models;

namespace TrelloCopy.Features.AuthManagement.ResetPassword.Commands
{
    public record ResetPasswordCommand(string ConfirmatinToken, string email, string NewPassword) : IRequest<RequestResult<bool>>;


    public class ResetPasswordCommandHandeler : BaseRequestHandler<ResetPasswordCommand, RequestResult<bool>, User>
    {
        public ResetPasswordCommandHandeler(BaseWithoutRepositoryRequestHandlerParameter<User> parameters) : base(parameters)
        {
        }

        public override async Task<RequestResult<bool>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
           var userExist = await  _mediator.Send(new GetUserIDIfPasswordTokenMatchQuery(request.email, request.ConfirmatinToken), cancellationToken);
           
           if (!userExist.isSuccess)
               return RequestResult<bool>.Failure(userExist.errorCode, userExist.message);
           
           var hasher = new PasswordHasher<string>();
           var Newpassword = hasher.HashPassword(null, request.NewPassword);
           
           var user = new User()
           {
                ID = userExist.data,
                Password = Newpassword,
           };
           
           await  _repository.SaveIncludeAsync(user, nameof(User.Password));
           
           await _repository.SaveChangesAsync(); 

           return RequestResult<bool>.Success(true);

         }
    }

}
