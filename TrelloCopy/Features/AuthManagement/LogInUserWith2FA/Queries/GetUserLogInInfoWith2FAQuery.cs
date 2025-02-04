using MediatR;
using Microsoft.EntityFrameworkCore;
using TrelloCopy.Common.BaseHandlers;
using TrelloCopy.Common.Data.Enums;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.AuthManagement.LogInUserWith2FA;
using TrelloCopy.Models;

namespace TrelloCopy.Features.AuthManagement.LogInUserWith2FA.Queries;
public record GetUserLogInInfoWith2FAQuery(string email) : IRequest<RequestResult<LogInInfoWith2FADTO>>;

public class GetUserLogInInfoWith2FAQueryHandler : BaseRequestHandler<GetUserLogInInfoWith2FAQuery, RequestResult<LogInInfoWith2FADTO>, User>
{
    public GetUserLogInInfoWith2FAQueryHandler (BaseWithoutRepositoryRequestHandlerParameter<User> parameters) : base(parameters)
    {
    }

    public override async Task<RequestResult<LogInInfoWith2FADTO>> Handle(GetUserLogInInfoWith2FAQuery request, CancellationToken cancellationToken)
    {
        var userData  = await _repository.Get(u => u.Email == request.email)
            .Select(u => new LogInInfoWith2FADTO(u.ID, u.TwoFactorAuthEnabled, u.TwoFactorAuthsecretKey)).FirstOrDefaultAsync();
        
        if (userData.ID <= 0)
        {
            return RequestResult<LogInInfoWith2FADTO>.Failure(ErrorCode.UserNotFound, "please check your email address.");
        }
        return RequestResult<LogInInfoWith2FADTO>.Success(userData);
    }
}