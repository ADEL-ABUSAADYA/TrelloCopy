using MediatR;
using Microsoft.EntityFrameworkCore;
using TrelloCopy.Common.BaseHandlers;
using TrelloCopy.Common.Data.Enums;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.AuthManagement.LogInUser;
using TrelloCopy.Models;

namespace TrelloCopy.Features.AuthManagement.LogInUser.Queries;
public record GetUserLogInInfoQuery(string email) : IRequest<RequestResult<LogInInfoDTO>>;

public class GetUserLogInInfoQueryHandler : BaseRequestHandler<GetUserLogInInfoQuery, RequestResult<LogInInfoDTO>, User>
{
    public GetUserLogInInfoQueryHandler (BaseRequestHandlerParameters<User> parameters) : base(parameters)
    {
    }

    public override async Task<RequestResult<LogInInfoDTO>> Handle(GetUserLogInInfoQuery request, CancellationToken cancellationToken)
    {
        var userData  = await _repository.Get(u => u.Email == request.email)
            .Select(u => new LogInInfoDTO(u.ID, u.TwoFactorAuthEnabled, u.Password, u.IsEmailConfirmed)).FirstOrDefaultAsync();
        
        if (userData.ID <= 0)
        {
            return RequestResult<LogInInfoDTO>.Failure(ErrorCode.UserNotFound, "please check your email address.");
        }
        return RequestResult<LogInInfoDTO>.Success(userData);
    }
}