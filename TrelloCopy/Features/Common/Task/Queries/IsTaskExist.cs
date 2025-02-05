using MediatR;
using TrelloCopy.Common.BaseHandlers;
using TrelloCopy.Common.Data.Enums;
using TrelloCopy.Common.Views;
using TrelloCopy.Models;

namespace TrelloCopy.Features.Common.Task.Queries
{
    public record IsTaskExist(int id):IRequest<RequestResult<bool>>;
    public class IsTaskExistQueryHandler : BaseRequestHandler<IsTaskExist, RequestResult<bool>, TaskEntity>
    {
        public IsTaskExistQueryHandler(BaseWithoutRepositoryRequestHandlerParameter<TaskEntity> parameters) : base(parameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(IsTaskExist request, CancellationToken cancellationToken)
        {
            var query = _repository.Get(c => c.ID == request.id && !c.Deleted).Select(c => new
            {
                c.ID
            }).FirstOrDefault();
            if (query == null) return RequestResult<bool>.Failure(ErrorCode.TaskNotFound, "there is no task with this id ");
            return RequestResult<bool>.Success(true);
        }
    }
}
