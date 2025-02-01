using MediatR;
using TrelloCopy.Common.BaseHandlers;
using TrelloCopy.Common.Data.Enums;
using TrelloCopy.Models;

namespace TrelloCopy.Features.Common.Users.Queries
{
    public record HasAccessQuery(int ID, Feature Feature) : IRequest<bool>;

    public class HasAccessQueryHandler : BaseRequestHandler<HasAccessQuery, bool, UserFeature>
    {
        public HasAccessQueryHandler(BaseRequestHandlerParameters<UserFeature> parameters) : base(parameters)
        {
        }
        public override async Task<bool> Handle(HasAccessQuery request, CancellationToken cancellationToken)
        {
            var hasFeature = await _repository.AnyAsync(
                uf => uf.UserID == request.ID && uf.Feature == request.Feature);
            return hasFeature;
        }
    }
}