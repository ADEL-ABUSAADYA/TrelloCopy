using MediatR;
using TrelloCopy.Common.BaseHandlers;
using TrelloCopy.Common.Data.Enums;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.AuthManagement.LogInUser.Queries;
using TrelloCopy.Models;
using OtpNet;
using TrelloCopy.Features.AuthManagement.LogInUserWith2FA.Queries;

namespace TrelloCopy.Features.AuthManagement.LogInUserWith2FA.Commands;

public record LogInUserWith2FACommand(string Email, string Otp) : IRequest<RequestResult<string>>;

public class LogInUserWith2FACommandHandler : BaseRequestHandler<LogInUserWith2FACommand, RequestResult<string>, User>
{
    public LogInUserWith2FACommandHandler(BaseWithoutRepositoryRequestHandlerParameter<User> parameters) : base(parameters)
    {
    }

    public override async Task<RequestResult<string>> Handle(LogInUserWith2FACommand request, CancellationToken cancellationToken)
    {
        var userInfo = await _mediator.Send(new GetUserLogInInfoWith2FAQuery(request.Email));
        if (!userInfo.isSuccess)
        {
            return RequestResult<string>.Failure(userInfo.errorCode, userInfo.message);
        }

        var user = userInfo.data;
        
        if (user.ID <= 0 || !user.Is2FAEnabled || string.IsNullOrEmpty(user.TwoFactorAuthsecretKey))
        {
            return RequestResult<string>.Failure(ErrorCode.UserNotFound, "User does not exist or 2FA is not enabled.");
        }

        // Validate the OTP using the secret key stored for the user
        var isOtpValid = Validate2FACode(user.TwoFactorAuthsecretKey, request.Otp);
        if (!isOtpValid)
        {
            return RequestResult<string>.Failure(ErrorCode.Invalid2FA, "Invalid OTP.");
        }

        // OTP is valid, issue a full JWT token for authentication
        var fullAccessToken = _tokenHelper.GenerateToken(user.ID);
        return RequestResult<string>.Success(fullAccessToken);
    }

    private bool Validate2FACode(string userSecretKey, string enteredOtp)
    {
        var keyBytes = Base32Encoding.ToBytes(userSecretKey);
        var totp = new Totp(keyBytes);
        
        return totp.VerifyTotp(enteredOtp, out long timeStepMatched);
    }
}