using MediatR;
using TrelloCopy.Common;
using TrelloCopy.Common.Data.Enums;
using TrelloCopy.Common.Views;
using TrelloCopy.Data.Repositories;
using TrelloCopy.Features.Common.Users.DTOs;
using TrelloCopy.Models;

namespace TrelloCopy.Features.UserManagement.RegisterUser.Queries;

public record IsUserExistQuery(string email) : IRequest<RequestResult<bool>>;


public class IsUserExistQueryHandler : UserBaseRequestHandler<IsUserExistQuery, RequestResult<bool>>
{
    public IsUserExistQueryHandler(UserBaseRequestHandlerParameters parameters) : base(parameters) { }

    public async override Task<RequestResult<bool>> Handle(IsUserExistQuery request, CancellationToken cancellationToken)
    {
        var result = await _userRepository.AnyAsync(u => u.Email == request.email);
        if (!result)
        {
            return RequestResult<bool>.Failure(ErrorCode.UserNotFound);
        }

        return RequestResult<bool>.Success(result);
    }
}