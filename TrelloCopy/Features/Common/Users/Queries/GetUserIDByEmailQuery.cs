using MediatR;
using Microsoft.EntityFrameworkCore;
using TrelloCopy.Common.BaseHandlers;
using TrelloCopy.Common.Data.Enums;
using TrelloCopy.Common.Views;
using TrelloCopy.Models;

namespace TrelloCopy.Features.Common.Users.Queries;

public record GetUserIDByEmailQuery(string Email) : IRequest<RequestResult<int>>;

public class GetUserIDByEmailQueryHandler : BaseRequestHandler<GetUserIDByEmailQuery, RequestResult<int>, User>
{
    public GetUserIDByEmailQueryHandler(BaseWithoutRepositoryRequestHandlerParameter<User> parameters) : base(parameters)
    {
    }

    public override async Task<RequestResult<int>> Handle(GetUserIDByEmailQuery request, CancellationToken cancellationToken)
    {
        var userID = await _repository.Get(U => U.Email == request.Email).Select(u => u.ID).FirstOrDefaultAsync();

        if(userID <= 0)
            return RequestResult<int>.Failure(ErrorCode.UserNotFound, "User does not exist");
        
        return RequestResult<int>.Success(userID);
    }
}