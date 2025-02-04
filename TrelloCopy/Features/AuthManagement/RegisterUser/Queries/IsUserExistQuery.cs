using MediatR;
using TrelloCopy.Common.BaseHandlers;
using TrelloCopy.Common.Data.Enums;
using TrelloCopy.Common.Views;
using TrelloCopy.Data.Repositories;
using TrelloCopy.Features.Common.Users.DTOs;
using TrelloCopy.Models;

namespace TrelloCopy.Features.AuthManagement.RegisterUser.Queries;

public record IsUserExistQuery (string email) : IRequest<RequestResult<bool>>;


public class IsUserExistQueryHandler : BaseRequestHandler<IsUserExistQuery, RequestResult<bool>, User>
{
    public IsUserExistQueryHandler(BaseWithoutRepositoryRequestHandlerParameter<User> parameters) : base(parameters) { }

    public override async Task<RequestResult<bool>> Handle(IsUserExistQuery request, CancellationToken cancellationToken)
    {
        var result= await _repository.AnyAsync(u => u.Email == request.email);
        if (!result)
        {
            return RequestResult<bool>.Failure(ErrorCode.UserNotFound);
        }

        return RequestResult<bool>.Success(result);
    }
}