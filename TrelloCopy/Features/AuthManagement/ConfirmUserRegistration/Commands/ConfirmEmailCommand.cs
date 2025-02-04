using MediatR;
using TrelloCopy.Common.BaseHandlers;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.AuthManagement.ConfirmUserRegistration.Queries;
using TrelloCopy.Models;

namespace TrelloCopy.Features.AuthManagement.ConfirmUserRegistration.Commands;

public record ConfirmEmailCommand(string email, string token) : IRequest<RequestResult<bool>>;

public class ConfirmEmailHandler : BaseRequestHandler<ConfirmEmailCommand, RequestResult<bool>, User>
{
    public ConfirmEmailHandler(BaseWithoutRepositoryRequestHandlerParameter<User> parameters) : base(parameters)
    {
    }

    public override async Task<RequestResult<bool>> Handle(ConfirmEmailCommand request,
        CancellationToken cancellationToken)
    {
        var isRegistered = await _mediator.Send(new IsUserRegisteredQuery(request.email, request.token));

        User user = new User();
        if (isRegistered.isSuccess)
        {
            user.ID = isRegistered.data;
            user.IsEmailConfirmed = true;
            user.ConfirmationToken = null;

            var result = await _repository.SaveIncludeAsync(user, nameof(User.IsEmailConfirmed),
                nameof(User.ConfirmationToken));

            await _repository.SaveChangesAsync();
            return RequestResult<bool>.Success(result);
        }

        return RequestResult<bool>.Failure(isRegistered.errorCode, isRegistered.message);
    }

    
}



