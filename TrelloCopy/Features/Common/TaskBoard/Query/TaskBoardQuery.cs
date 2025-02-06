using MediatR;
using Microsoft.EntityFrameworkCore;
using TrelloCopy.Common.BaseHandlers;
using TrelloCopy.Common.Data.Enums;
using TrelloCopy.Common.Helper;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.TaskManagement.Tasks.GetAllTasks.Query;
using TrelloCopy.Models;

namespace TrelloCopy.Features.Common.TaskBoard.Query
{

    public record TaskBoardQuery(int id) : IRequest<RequestResult<TaskBoradDTO>>;
    public class TaskBoardQueryHandler : BaseRequestHandler<TaskBoardQuery, RequestResult<TaskBoradDTO>, TaskEntity>
    {
        public TaskBoardQueryHandler(BaseWithoutRepositoryRequestHandlerParameter<TaskEntity> parameters) : base(parameters)
        {
        }

        public async override Task<RequestResult<TaskBoradDTO>> Handle(TaskBoardQuery request, CancellationToken cancellationToken)
        {
            var allTasks= await _repository.Get(c=> c.ProjectId==request.id).Select(c=> new
            {
                c.Title,
                c.Status,
            }).ToListAsync();

            if (!allTasks.Any()) return RequestResult<TaskBoradDTO>.Failure(ErrorCode.TaskNotFound, "there is no tasks i this project"); 

            var inprogress =  allTasks.Where(c => c.Status.ToString() == "InProgress"); 
            
            var done  =  allTasks.Where(c=> c.Status.ToString() == "Done");


            var board = new TaskBoradDTO
            {
                ToDO = allTasks.Select(c => c.Title).ToList(),
                InProgress = inprogress.Select(c => c.Title).ToList(),
                Done = done.Select(c => c.Title).ToList(),
            };

            return RequestResult<TaskBoradDTO>.Success(board); 


        }
    }
}
