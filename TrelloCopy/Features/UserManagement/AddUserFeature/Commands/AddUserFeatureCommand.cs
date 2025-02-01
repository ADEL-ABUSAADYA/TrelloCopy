using MediatR;
using TrelloCopy.Common.BaseHandlers;
using TrelloCopy.Common.Data.Enums; 
using TrelloCopy.Common.Views;
using TrelloCopy.Features.Common.Users.Queries;
using TrelloCopy.Models;

namespace TrelloCopy.Features.UserManagement.AddUserFeature.Commands;

public record AddUserFeatureCommand(string Email, Feature feature) : IRequest<RequestResult<bool>>;

public class AddUserFeatureCommandHandler : BaseRequestHandler<AddUserFeatureCommand, RequestResult<bool>, UserFeature>
{
    public AddUserFeatureCommandHandler(BaseRequestHandlerParameters<UserFeature> parameters ): base(parameters) { }

    public async override Task<RequestResult<bool>> Handle(AddUserFeatureCommand request, CancellationToken cancellationToken)
    {
        
        var userID = await _mediator.Send(new GetUserIDByEmailQuery(request.Email));
        if (!userID.isSuccess)
            return RequestResult<bool>.Failure(userID.errorCode, userID.message);

        var hasAccess = await _mediator.Send(new HasAccessQuery(userID.data, request.feature));
        if (hasAccess)
            return RequestResult<bool>.Success(true);
        
        await _repository.AddAsync(new UserFeature
            {
                Feature = request.feature,
                UserID = userID.data,
            });
        await _repository.SaveChangesAsync();

        return RequestResult<bool>.Success(true);
    }
}
