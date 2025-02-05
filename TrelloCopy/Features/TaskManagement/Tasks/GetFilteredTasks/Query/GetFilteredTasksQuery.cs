using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TrelloCopy.Common.BaseHandlers;
using TrelloCopy.Common.Helper;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.TaskManagement.Tasks.GetAllTasks.Query;
using TrelloCopy.Models;

namespace TrelloCopy.Features.TaskManagement.Tasks.GetTasksByStatus.Query
{
    public record GetFilteredTasksQuery(int PageNumber , int PageSize ,Dictionary<string , object> Filters) : IRequest<RequestResult<Pagination<GetTaskDto>>>;
 
    public class GetFilteredTasksQueryHandler(BaseWithoutRepositoryRequestHandlerParameter<TaskEntity> parameters)
        : BaseRequestHandler<GetFilteredTasksQuery, RequestResult<Pagination<GetTaskDto>>, TaskEntity>(parameters)
    {
        public async override Task<RequestResult<Pagination<GetTaskDto>>> Handle(GetFilteredTasksQuery request, CancellationToken cancellationToken)
        {
            var tasks = _repository.GetAll();
            var parameter = Expression.Parameter(typeof(TaskEntity), "t");
            Expression expression = Expression.Constant(true);

            foreach (var filter in request.Filters)
            {
                var property = Expression.Property(parameter, filter.Key);
                var value = Expression.Constant(Convert.ChangeType(filter.Value, property.Type));

                var equalsExpression = Expression.Equal(property, value);
                expression = Expression.AndAlso(expression, equalsExpression);
            }

            var lambda = Expression.Lambda<Func<TaskEntity, bool>>(expression, parameter);
            var query = tasks.Where(lambda)
                .Include(u=>u.User)
                .Include(u=>u.Project)
                .Select(t=>new GetTaskDto(t.Title,t.Description,t.User.Name,t.Status.ToString(),t.Project.Title,t.CreatedDate)
            );
            var paginatedResult = await Pagination<GetTaskDto>.ToPagedList(query, request.PageNumber, request.PageSize);
            return RequestResult<Pagination<GetTaskDto>>.Success(paginatedResult);

        }
    }
}
