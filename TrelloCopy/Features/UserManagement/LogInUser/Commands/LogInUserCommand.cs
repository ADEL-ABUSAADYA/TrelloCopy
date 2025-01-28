using MediatR;
using Microsoft.AspNetCore.Identity;
using TrelloCopy.Common;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.userManagement.LogInUser.Queries;

namespace TrelloCopy.Features.UserManagement.LogInUser.Commands;

public record LogInUserCommand(string Email, string Password) : IRequest<RequestResult<string>>;

public class LogInUserCommandHandler : UserBaseRequestHandler<LogInUserCommand, RequestResult<string>>
{
    public LogInUserCommandHandler(UserBaseRequestHandlerParameters parameters) : base(parameters)
    {
    }

    public override async Task<RequestResult<string>> Handle(LogInUserCommand request, CancellationToken cancellationToken)
    {
        PasswordHasher<string> passwordHasher = new PasswordHasher<string>();
        var password = passwordHasher.HashPassword(null, request.Password);
        
        var userInfo = await _mediator.Send(new GetUserLogInInfoQuery(request.Email, password));
        if (!userInfo.isSuccess)
        {
            return RequestResult<string>.Failure(userInfo.errorCode, userInfo.message);
        }
        
        if (userInfo.data.ID != 0 && !userInfo.data.IsAuthenticationEnabled)
        {
            var token = _tokenHelper.GenerateToken(userInfo.data.ID);
            return RequestResult<string>.Success(token);
        }
        if (userInfo.data.IsAuthenticationEnabled)
        {
            return RequestResult<string>.Success(null, "Two-factor authentication required.");
        }

        return RequestResult<string>.Failure(userInfo.errorCode, userInfo.message);
    }
}