using MediatR;
using Microsoft.AspNetCore.Identity;
using OtpNet;
using TrelloCopy.Common;
using TrelloCopy.Common.Data.Enums;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.Common.Users.Queries;
using TrelloCopy.Models;


namespace TrelloCopy.Features.UserManagement.ActivateUser2FA.Commands;

public record ActivateUser2FAOrchestrator() : IRequest<RequestResult<string>>;

public class ActivateUser2FAOrchestratorHandler : BaseRequestHandler<ActivateUser2FAOrchestrator, RequestResult<string>, BaseModel>
{
    public ActivateUser2FAOrchestratorHandler(BaseRequestHandlerParameters<BaseModel> parameters) : base(parameters)
    {
    }

    public override async Task<RequestResult<string>> Handle(ActivateUser2FAOrchestrator request, CancellationToken cancellationToken)
    {
        var user2FADataCommand = await _mediator.Send(new GetUser2FAInfoQuery());
        if (!user2FADataCommand.isSuccess)
        {
            return RequestResult<string>.Failure(user2FADataCommand.errorCode, user2FADataCommand.message);
        }
        if (user2FADataCommand.data.Is2FAEnabled)
        {
            return RequestResult<string>.Failure(ErrorCode.Uasr2FAIsNotEnabled, "Two-Factor Authentication is already enabled for this user.");
        }
        
        var appName = "UpSkilling-ProjectManagement-JSB2";
        string otpUrl;

        if (user2FADataCommand.data.Is2FAEnabled && !string.IsNullOrEmpty(user2FADataCommand.data.TwoFactorAuthSecretKey))
        {
            otpUrl = $"otpauth://totp/{appName}:{_userInfo.ID}?secret={user2FADataCommand.data.TwoFactorAuthSecretKey}&issuer={appName}";
        }
        else
        {
            var secretKey = Base32Encoding.ToString(KeyGeneration.GenerateRandomKey(20));
            
            var updateResult = await _mediator.Send(new UpdateUser2FASecretKeyCommand(secretKey));
            if (!updateResult.isSuccess)
            {
                return RequestResult<string>.Failure(updateResult.errorCode, updateResult.message);
            }

            otpUrl = $"otpauth://totp/{appName}:{_userInfo.ID}?secret={secretKey}&issuer={appName}";
        }

        return RequestResult<string>.Success(otpUrl);
    }
}