using MediatR;
using Microsoft.AspNetCore.Identity;
using TrelloCopy.Common.BaseHandlers;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.Common.Users.DTOs;
using TrelloCopy.Features.AuthManagement.LogInUser.Queries;
using TrelloCopy.Models;

namespace TrelloCopy.Features.AuthManagement.LogInUser.Commands;

public record LogInUserCommand(string Email, string Password) : IRequest<RequestResult<TokenDTO>>;

public class LogInUserCommandHandler : BaseRequestHandler<LogInUserCommand, RequestResult<TokenDTO>, User>
{
    public LogInUserCommandHandler(BaseRequestHandlerParameters<User> parameters) : base(parameters)
    {
    }

    public override async Task<RequestResult<TokenDTO>> Handle(LogInUserCommand request, CancellationToken cancellationToken)
    {
        
        var userInfo = await _mediator.Send(new GetUserLogInInfoQuery(request.Email));
        if (!userInfo.isSuccess)
        {
            return RequestResult<TokenDTO>.Failure(userInfo.errorCode, userInfo.message);
        }
        var isPasswordMached = CheckPassword(request.Password, userInfo.data.hashedPassword);
        if (!isPasswordMached)
            return RequestResult<TokenDTO>.Failure(userInfo.errorCode, "password is incorrect.");
        if (!userInfo.data.IsEmailConfirmed)
        {
            return RequestResult<TokenDTO>.Failure(userInfo.errorCode, "email is not confirmed.");
        }
        if (userInfo.data.ID > 0 && !userInfo.data.Is2FAEnabled)
        {
            var token = _tokenHelper.GenerateToken(userInfo.data.ID);
            return RequestResult<TokenDTO>.Success(new TokenDTO(LogInToken: token, TokenWith2FA: ""));
        }
        if (userInfo.data.ID > 0 && userInfo.data.Is2FAEnabled)
        {
            var token2fa = _tokenHelper.Generate2FALoginToken(userInfo.data.ID);
            return RequestResult<TokenDTO>.Success(new TokenDTO(LogInToken: "", TokenWith2FA: token2fa));
        }

        return RequestResult<TokenDTO>.Failure(userInfo.errorCode, userInfo.message);
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