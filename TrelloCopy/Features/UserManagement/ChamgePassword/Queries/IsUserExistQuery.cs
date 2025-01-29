using MediatR;
using TrelloCopy.Common;
using TrelloCopy.Common.Data.Enums;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.Common.Users.DTOs;
using TrelloCopy.Features.UserManagement.LogInUser;
using TrelloCopy.Models;

namespace TrelloCopy.Features.UserManagement.ChangePassword.Queries;

public record IsUserExistQuery (int userId) : IRequest<RequestResult<User>>;

public class IsUserExistQueryHandler : UserBaseRequestHandler<IsUserExistQuery, RequestResult<User>>
{
    public IsUserExistQueryHandler(UserBaseRequestHandlerParameters parameters) : base(parameters)
    {
    }

    public override async Task<RequestResult<User>> Handle(IsUserExistQuery request, CancellationToken cancellationToken)
    {
        var isExist = await _userRepository.GetByIDAsync(request.userId);
        if (isExist is null)
            return RequestResult<User>.Failure(ErrorCode.UserNotFound);

        return RequestResult<User>.Success(isExist);


    }
}

