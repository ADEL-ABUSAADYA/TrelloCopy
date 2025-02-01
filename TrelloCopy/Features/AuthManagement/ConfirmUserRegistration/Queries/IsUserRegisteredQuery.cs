using MediatR;
using Microsoft.EntityFrameworkCore;
using TrelloCopy.Common.BaseHandlers;
using TrelloCopy.Common.Data.Enums;
using TrelloCopy.Common.Views;
using TrelloCopy.Models;

namespace TrelloCopy.Features.AuthManagement.ConfirmUserRegistration.Queries;

public record IsUserRegisteredQuery(string email, string token) : IRequest<RequestResult<int>>;

public class IsUserRegisteredQueryHandler : BaseRequestHandler<IsUserRegisteredQuery, RequestResult<int>, User>
{
    public IsUserRegisteredQueryHandler(BaseRequestHandlerParameters<User> parameters) : base(parameters)
    {
    }

    public override async Task<RequestResult<int>> Handle(IsUserRegisteredQuery request, CancellationToken cancellationToken)
    {
        var result= await _repository.Get(u => u.Email == request.email && u.ConfirmationToken == request.token).Select(u => u.ID).FirstOrDefaultAsync();
        if (result == 0)
        {
            return RequestResult<int>.Failure(ErrorCode.UserNotFound);
        }
        return RequestResult<int>.Success(result);
    }
}