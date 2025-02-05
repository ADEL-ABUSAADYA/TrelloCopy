using MediatR;
using Microsoft.EntityFrameworkCore;
using TrelloCopy.Common.BaseHandlers;
using TrelloCopy.Common.Data.Enums;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.Common.Pagination;
using TrelloCopy.Models;

namespace TrelloCopy.Features.UserManagement.BlockUser.Queries;

public record CheckUserActivation(int PageNumber , int PageSize) : IRequest<RequestResult<bool>>;

public class CheckUserActivationHandler : BaseRequestHandler<CheckUserActivation, RequestResult<bool>, User>
{
    public CheckUserActivationHandler(BaseWithoutRepositoryRequestHandlerParameter<User> parameters) : base(parameters)
    {
    }

    public override Task<RequestResult<bool>> Handle(CheckUserActivation request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}