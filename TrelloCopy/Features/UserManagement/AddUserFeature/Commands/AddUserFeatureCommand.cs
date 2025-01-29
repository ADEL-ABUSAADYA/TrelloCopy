using MediatR;
using TrelloCopy.Common;
using TrelloCopy.Common.Data.Enums; 
using TrelloCopy.Common.Views;
using TrelloCopy.Models;

namespace TrelloCopy.Features.userManagement.AddUserFeature.Commands;

public record AddUserFeatureCommand(int userID, Feature feature) : IRequest<RequestResult<bool>>;

public class AddUserFeatureCommandHandler : BaseRequestHandler<AddUserFeatureCommand, RequestResult<bool>, UserFeature>
{
    public AddUserFeatureCommandHandler(BaseRequestHandlerParameters<UserFeature> parameters ): base(parameters) { }

    public async override Task<RequestResult<bool>> Handle(AddUserFeatureCommand request, CancellationToken cancellationToken)
    {
        var userFeatureID = await _repository.AddAsync(new UserFeature
        {
            Feature = request.feature
        });
         await _repository.SaveChangesAsync();

         if (userFeatureID >= 0)
         {
             return RequestResult<bool>.Success(true);
         }

         return RequestResult<bool>.Failure(ErrorCode.UserNotFound);
    }
}