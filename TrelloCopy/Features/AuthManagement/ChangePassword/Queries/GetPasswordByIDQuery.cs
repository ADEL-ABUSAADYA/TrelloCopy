using MediatR;
using Microsoft.EntityFrameworkCore;
using TrelloCopy.Common.BaseHandlers;
using TrelloCopy.Common.Data.Enums;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.Common.Users.DTOs;
using TrelloCopy.Features.AuthManagement.LogInUser;
using TrelloCopy.Models;

namespace TrelloCopy.Features.AuthManagement.ChangePassword.Queries;

public record GetPasswordByIDQuery () : IRequest<RequestResult<string>>;

public class GetPasswordByIDQueryHandler : BaseRequestHandler<GetPasswordByIDQuery, RequestResult<string>, User>
{
    public GetPasswordByIDQueryHandler(BaseRequestHandlerParameters<User> parameters) : base(parameters)
    {
    }

    public override async Task<RequestResult<string>> Handle(GetPasswordByIDQuery request, CancellationToken cancellationToken)
    {
        var password = await _repository.Get(u => u.ID == _userInfo.ID).Select(u => u.Password).FirstOrDefaultAsync();
        
        if (string.IsNullOrWhiteSpace(password))
            return RequestResult<string>.Failure(ErrorCode.PasswordTokenNotMatch, "Password Token Not Match");

        return RequestResult<string>.Success(password);


    }
}

