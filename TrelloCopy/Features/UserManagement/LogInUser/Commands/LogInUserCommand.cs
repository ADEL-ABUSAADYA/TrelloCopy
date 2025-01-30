using MediatR;
using Microsoft.AspNetCore.Identity;
using TrelloCopy.Common;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.userManagement.LogInUser.Queries;
using TrelloCopy.Models;

namespace TrelloCopy.Features.UserManagement.LogInUser.Commands;

public record LogInUserCommand(string Email, string Password) : IRequest<RequestResult<string>>;

public class LogInUserCommandHandler : BaseRequestHandler<LogInUserCommand, RequestResult<string>, User>
{
    public LogInUserCommandHandler(BaseRequestHandlerParameters<User> parameters) : base(parameters)
    {
    }

    public override async Task<RequestResult<string>> Handle(LogInUserCommand request, CancellationToken cancellationToken)
    {
        
        var userInfo = await _mediator.Send(new GetUserLogInInfoQuery(request.Email));
        if (!userInfo.isSuccess)
        {
            return RequestResult<string>.Failure(userInfo.errorCode, userInfo.message);
        }
        var isPasswordMached = CheckPassword(request.Password, userInfo.data.hashedPassword);
        if (!isPasswordMached)
            return RequestResult<string>.Failure(userInfo.errorCode, "password is incorrect.");
        
        if (userInfo.data.ID > 0 && !userInfo.data.Is2FAEnabled)
        {
            var token = _tokenHelper.GenerateToken(userInfo.data.ID);
            return RequestResult<string>.Success(token);
        }
        if (userInfo.data.ID > 0 && userInfo.data.Is2FAEnabled)
        {
            var token2fa = _tokenHelper.Generate2FALoginToken(userInfo.data.ID);
            return RequestResult<string>.Success(token2fa);
        }

        return RequestResult<string>.Failure(userInfo.errorCode, userInfo.message);
    }

    private bool CheckPassword(string requestPassword, string databasePassword)
    {
        PasswordHasher<string> passwordHasher = new PasswordHasher<string>();
        var passwordVerificationResult = passwordHasher.VerifyHashedPassword(null, databasePassword, requestPassword);
        if (passwordVerificationResult== PasswordVerificationResult.Success)
        return true;
        
        return false;
    }
}