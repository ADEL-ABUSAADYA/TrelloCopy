using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using TrelloCopy.Common;
using TrelloCopy.Common.Data.Enums;
using TrelloCopy.Common.Views;
using TrelloCopy.Models;

namespace TrelloCopy.Features.UserManagement.ResetPassword.Commands
{
    public record ResetPasswordCommand(string ConfirmatinToken  , string email , string NewPassword) : IRequest<RequestResult<bool>>;


    public class ResetPasswordCommandHandeler : BaseRequestHandler<ResetPasswordCommand, RequestResult<bool>, User>
    {
        public ResetPasswordCommandHandeler(BaseRequestHandlerParameters<User> parameters) : base(parameters)
        {
        }

        public override async Task<RequestResult<bool>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
           // var userExist = await  _repository.Get(u=> u.Email == request.email && u.ResetPassword == request.otp).FirstOrDefaultAsync();

           //  if (userExist == null) return RequestResult<bool>.Failure(ErrorCode.UserNotFound, "ples check is the email exsit or otp that genetrate doesnot expire");
           //
           //  var hasher = new PasswordHasher<string>();
           //
           //  var Newpassword = hasher.HashPassword(null, request.NewPassword);
           //
           //  var userr = new User()
           //  {
           //      ID = userExist.ID,
           //      Password = Newpassword,
           //  };
           //
           // await  _userRepository.SaveIncludeAsync(userr, nameof(userr.Password));
           //
           //  await _userRepository.SaveChangesAsync(); 

            return RequestResult<bool>.Success(true);

         }
    }

}
