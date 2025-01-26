using MediatR;
using TrelloCopy.Common;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.UserManagement.ConfirmUserRegistration.Queries;
using TrelloCopy.Models;

namespace TrelloCopy.Features.UserManagement.ConfirmUserRegistration.Commands;

public record ConfirmEmailCommand(string email, string token) : IRequest<RequestResult<bool>>;

public class ConfirmEmailHandler : UserBaseRequestHandler<ConfirmEmailCommand, RequestResult<bool>>
{
    public ConfirmEmailHandler(UserBaseRequestHandlerParameters parameters) : base(parameters)
    {
    }

    public override async Task<RequestResult<bool>> Handle(ConfirmEmailCommand request,
        CancellationToken cancellationToken)
    {
        var isRegistered = await _mediator.Send(new IsUserRegisteredQuery(request.email, request.token));

        if (isRegistered.isSuccess)
        {
            var user = new User
            {
                ID = isRegistered.data,
                IsEmailConfirmed = true,
                ConfirmationToken = null
            };
            await _userRepository.SaveIncludeAsync(user, nameof(user.IsEmailConfirmed), nameof(user.ConfirmationToken));

            return RequestResult<bool>.Success(user.IsEmailConfirmed);
        }
        
        return RequestResult<bool>.Failure(isRegistered.errorCode, isRegistered.message);
    }
}

