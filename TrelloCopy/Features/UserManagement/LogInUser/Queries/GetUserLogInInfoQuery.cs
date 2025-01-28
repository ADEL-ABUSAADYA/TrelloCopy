using MediatR;
using Microsoft.EntityFrameworkCore;
using TrelloCopy.Common;
using TrelloCopy.Common.Data.Enums;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.UserManagement.LogInUser;

namespace TrelloCopy.Features.userManagement.LogInUser.Queries;
public record GetUserLogInInfoQuery(string email) : IRequest<RequestResult<LogInInfoDTO>>;

public class GetUserLogInInfoQueryHandler : UserBaseRequestHandler<GetUserLogInInfoQuery, RequestResult<LogInInfoDTO>>
{
    public GetUserLogInInfoQueryHandler (UserBaseRequestHandlerParameters parameters) : base(parameters)
    {
    }

    public override async Task<RequestResult<LogInInfoDTO>> Handle(GetUserLogInInfoQuery request, CancellationToken cancellationToken)
    {
        var userData  = await _userRepository.Get(u => u.Email == request.email)
            .Select(u => new LogInInfoDTO(u.ID, u.TwoFactorAuthEnabled, u.Password)).FirstOrDefaultAsync();
        
        if (userData.ID <= 0)
        {
            return RequestResult<LogInInfoDTO>.Failure(ErrorCode.UserNotFound, "please check your email address.");
        }
        return RequestResult<LogInInfoDTO>.Success(userData);
    }
}