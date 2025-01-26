using MediatR;
using TrelloCopy.Common;
using TrelloCopy.Common.Data.Enums;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.UserManagement.ReSendRegistrationEmail.Queries;


namespace TrelloCopy.Features.UserManagement.ReSendRegistrationEmail.Commands;

public record ReSendRegistrationEmailCommand(string email) : IRequest<RequestResult<bool>>;

public class ReSendRegistrationEmailCommandHandler : UserBaseRequestHandler<ReSendRegistrationEmailCommand, RequestResult<bool>>
{
    public ReSendRegistrationEmailCommandHandler(UserBaseRequestHandlerParameters parameters) : base(parameters)
    {
    }

    public override async Task<RequestResult<bool>> Handle(ReSendRegistrationEmailCommand request,
        CancellationToken cancellationToken)
    {
        var isRegistered = await _mediator.Send(new GetUserRegistrationInfoQuery(request.email));

        if (isRegistered.data.IsRegistered)
            return RequestResult<bool>.Failure(ErrorCode.UserAlreadyRegistered, "user already registered please login");
  
        
        return RequestResult<bool>.Failure(isRegistered.errorCode, isRegistered.message);
    }
}

