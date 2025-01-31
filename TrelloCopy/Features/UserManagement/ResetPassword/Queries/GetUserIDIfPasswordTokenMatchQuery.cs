using MediatR;
using Microsoft.EntityFrameworkCore;
using TrelloCopy.Common;
using TrelloCopy.Common.Data.Enums;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.Common.Users.Queries;
using TrelloCopy.Models;

namespace TrelloCopy.Features.UserManagement.ResetPassword.Queries;

public record GetUserIDIfPasswordTokenMatchQuery(string Email, string PasswordToken) : IRequest<RequestResult<int>>;

public class GetUserIDIfPasswordTokenMatchQueryHandler : BaseRequestHandler<GetUserIDIfPasswordTokenMatchQuery, RequestResult<int>, User>
{
    public GetUserIDIfPasswordTokenMatchQueryHandler(BaseRequestHandlerParameters<User> parameters) : base(parameters)
    {
    }

    public override async Task<RequestResult<int>> Handle(GetUserIDIfPasswordTokenMatchQuery request, CancellationToken cancellationToken)
    {
        var userID = await _repository.Get(U => U.Email == request.Email && U.ResetPasswordToken == request.PasswordToken).Select(u => u.ID).FirstOrDefaultAsync();

        if(userID <= 0)
            return RequestResult<int>.Failure(ErrorCode.UserNotFound, "User does not exist");
        
        return RequestResult<int>.Success(userID);
    }
}