using MediatR;
using Microsoft.AspNetCore.Identity;
using OtpNet;
using TrelloCopy.Common.BaseHandlers;
using TrelloCopy.Common.Data.Enums;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.Common.Users.Queries;
using TrelloCopy.Models;


public record UpdateUser2FASecretKeyCommand(string User2FASecretKey) : IRequest<RequestResult<bool>>;

public class UpdateUser2FASecretKeyCommandHandler : BaseRequestHandler<UpdateUser2FASecretKeyCommand, RequestResult<bool>, User>
{
    public UpdateUser2FASecretKeyCommandHandler(BaseRequestHandlerParameters<User> parameters) : base(parameters)
    {
    }

    public override async Task<RequestResult<bool>> Handle(UpdateUser2FASecretKeyCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            ID = _userInfo.ID,
            TwoFactorAuthsecretKey = request.User2FASecretKey,
            TwoFactorAuthEnabled = true,
        };

        try
        {

            _repository.SaveIncludeAsync(user, nameof(user.TwoFactorAuthsecretKey), nameof(user.TwoFactorAuthEnabled));
            _repository.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return RequestResult<bool>.Failure(ErrorCode.UnKnownError, e.Message);
        }

        return RequestResult<bool>.Success(true);
    }
}