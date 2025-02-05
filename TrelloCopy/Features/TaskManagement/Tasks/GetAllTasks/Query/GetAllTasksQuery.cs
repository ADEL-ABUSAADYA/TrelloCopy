using MediatR;
using TrelloCopy.Common.BaseHandlers;
using TrelloCopy.Common.Data.Enums;
using TrelloCopy.Common.Helper;
using TrelloCopy.Common.Views;
using TrelloCopy.Models;

namespace TrelloCopy.Features.TaskManagement.Tasks.GetAllTasks.Query
{
    public record GetAllTasksQuery(int PageNumber, int PageSize) : IRequest<RequestResult<Pagination<GetTaskDto>>>;
    public class GetAllTasksQueryHandler : BaseRequestHandler<GetAllTasksQuery, RequestResult<Pagination<GetTaskDto>>, TaskEntity>
    {
        public GetAllTasksQueryHandler(BaseWithoutRepositoryRequestHandlerParameter<TaskEntity> parameters) : base(parameters)
        {
        }

        public async override Task<RequestResult<Pagination<GetTaskDto>>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
        {
            var data = _repository.GetAll();
            if (!data.Any()) return RequestResult<Pagination<GetTaskDto>>.Failure(ErrorCode.NoTaskAdd);
            var tasks = data.Select(c => new GetTaskDto(c.Title, c.Description, c.User.Name, c.Status.ToString(), c.Project.Title, c.CreatedDate));
            var paginatedResult = await Pagination<GetTaskDto>.ToPagedList(tasks, request.PageNumber, request.PageSize);
            return RequestResult<Pagination<GetTaskDto>>.Success(paginatedResult);
        }
    }
}
