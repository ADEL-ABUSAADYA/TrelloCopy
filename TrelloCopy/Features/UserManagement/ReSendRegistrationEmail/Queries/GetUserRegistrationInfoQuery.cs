using MediatR;
using Microsoft.EntityFrameworkCore;
using TrelloCopy.Common;
using TrelloCopy.Common.Data.Enums;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.UserManagement.ResendRegistrationEmail;
using TrelloCopy.Models;

namespace TrelloCopy.Features.UserManagement.ReSendRegistrationEmail.Queries;

public record GetUserRegistrationInfoQuery(string email) : IRequest<RequestResult<RegistrationInfoDTO>>;

public class GetUserRegistrationInfoQueryHandler : BaseRequestHandler<GetUserRegistrationInfoQuery, RequestResult<RegistrationInfoDTO>, User>
{
    public GetUserRegistrationInfoQueryHandler(BaseRequestHandlerParameters<User> parameters) : base(parameters)
    {
    }

    public override async Task<RequestResult<RegistrationInfoDTO>> Handle(GetUserRegistrationInfoQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.Get(u => u.Email == request.email).Select(u => new RegistrationInfoDTO
        {
            Name = u.Name,
            ConfirmationToken = u.ConfirmationToken,
            Email = u.Email,
            IsRegistered = u.IsEmailConfirmed
        }).FirstOrDefaultAsync();
        
        if (result == null)
            return RequestResult<RegistrationInfoDTO>.Failure(ErrorCode.UserNotFound, "please check your email address or register your email address.");
        
        return RequestResult<RegistrationInfoDTO>.Success(result);
    }
}