using MediatR;
using Microsoft.AspNetCore.Identity;
using TrelloCopy.Common;
using TrelloCopy.Common.Data.Enums;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.UserManagement.ChangePassword.Queries;
using TrelloCopy.Models;

namespace TrelloCopy.Features.UserManagement.ChangePassword.Commands;
public record ChangePasswordCommand(int userId,string password,string newPassword) : IRequest<RequestResult<bool>>;
public class ChangePasswordCommandHandler : BaseRequestHandler<ChangePasswordCommand, RequestResult<bool>, User>
{
    public ChangePasswordCommandHandler(BaseRequestHandlerParameters<User> parameters) : base(parameters)
    {
    }

    public async override Task<RequestResult<bool>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new IsUserExistQuery(request.userId));

        if (!response.isSuccess)
            return RequestResult<bool>.Failure(ErrorCode.UserNotFound);

        PasswordHasher<string> passwordHasher = new PasswordHasher<string>();

        var newPassword = passwordHasher.HashPassword(null, request.newPassword);
        var user = response.data;

        var isOldPasswordCorrect = CheckPassword(request.password, user.Password);
        if (!isOldPasswordCorrect)
            return RequestResult<bool>.Failure(ErrorCode.InvalidInput);

        user.Password = newPassword;
         await _repository.SaveIncludeAsync(user,"Password");
       await _repository.SaveChangesAsync();

          return RequestResult<bool>.Success(true);

    }

    private bool CheckPassword(string requestPassword, string databasePassword)
    {
        var passwordHasher = new PasswordHasher<string>();
        return passwordHasher.VerifyHashedPassword(null, databasePassword, requestPassword) != PasswordVerificationResult.Failed;
    }

}
