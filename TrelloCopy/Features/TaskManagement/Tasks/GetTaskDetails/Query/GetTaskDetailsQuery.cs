using MediatR;
using Microsoft.EntityFrameworkCore;
using TrelloCopy.Common.BaseHandlers;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.Common.Task.Queries;
using TrelloCopy.Features.TaskManagement.Tasks.GetAllTasks.Query;
using TrelloCopy.Models;

namespace TrelloCopy.Features.TaskManagement.Tasks.GetTaskDetails.Query
{
    public record GetTaskDetailsQuery(int id) : IRequest<RequestResult<GetTaskDto>>;
    public class GetTasktDetailsQueryHandler : BaseRequestHandler<GetTaskDetailsQuery, RequestResult<GetTaskDto>, TaskEntity>
    {
        public GetTasktDetailsQueryHandler(BaseWithoutRepositoryRequestHandlerParameter<TaskEntity> parameters) : base(parameters)
        {
        }

        public override async Task<RequestResult<GetTaskDto>> Handle(GetTaskDetailsQuery request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new IsTaskExist(request.id));
            if(!response.isSuccess)
            {
                return RequestResult<GetTaskDto>.Failure(response.errorCode, response.message);
            }
            var query = await _repository.Get(c => c.ID == request.id && !c.Deleted)
                .Include(u=>u.User)
                .Include(p=>p.Project)
                .Select(c => new GetTaskDto(c.Title,c.Description,c.User.Name,c.Status.ToString(),c.Project.Title,c.CreatedDate)
            ).FirstOrDefaultAsync();
            return RequestResult<GetTaskDto>.Success(query!);
        }
    }

}
