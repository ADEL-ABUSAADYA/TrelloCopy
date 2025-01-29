using MediatR;
using Microsoft.EntityFrameworkCore;
using TrelloCopy.Common;
using TrelloCopy.Common.Data.Enums;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.Common.Users.DTOs;
using TrelloCopy.Models;


namespace TrelloCopy.Features.Common.Users.Queries;
public record GetUser2FAInfoQuery() : IRequest<RequestResult<User2FAInfoDTO>>;

public class GetUser2FAInfoQueryHandler : BaseRequestHandler<GetUser2FAInfoQuery, RequestResult<User2FAInfoDTO>, User>
{
    public GetUser2FAInfoQueryHandler (BaseRequestHandlerParameters<User> parameters) : base(parameters)
    {
    }

    public override async Task<RequestResult<User2FAInfoDTO>> Handle(GetUser2FAInfoQuery request, CancellationToken cancellationToken)
    {
        var userData  = await _repository.Get(u => u.ID == _userInfo.ID)
            .Select(u => new User2FAInfoDTO(u.TwoFactorAuthEnabled, u.TwoFactorAuthsecretKey)).FirstOrDefaultAsync();
        
        if (userData is null)
        {
            return RequestResult<User2FAInfoDTO>.Failure(ErrorCode.UserNotFound, "please login again");
        }
        return RequestResult<User2FAInfoDTO>.Success(userData);
    }
}