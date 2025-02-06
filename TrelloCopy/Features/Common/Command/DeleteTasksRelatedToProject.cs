using MediatR;
using Microsoft.EntityFrameworkCore;
using TrelloCopy.Common.BaseHandlers;
using TrelloCopy.Common.Data.Enums;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.Common.Projects.Queries;
using TrelloCopy.Features.ProjectManage.AddProject.Command;
using TrelloCopy.Models;

namespace TrelloCopy.Features.Common.Command
{
    public record DeleteTasksRelatedToProject (int projectID) :IRequest<RequestResult<bool>>;


    public class DeleteTasksRelatedToProjectHandler : BaseRequestHandler<DeleteTasksRelatedToProject, RequestResult<bool>, TaskEntity>
    {
        public DeleteTasksRelatedToProjectHandler(BaseWithoutRepositoryRequestHandlerParameter<TaskEntity> parameters) : base(parameters)
        {


        }

        public override async Task<RequestResult<bool>> Handle(DeleteTasksRelatedToProject request, CancellationToken cancellationToken)
        {

            var tasks =  _repository.
                Get(c => c.ProjectId == request.projectID && !c.Deleted).
                ExecuteUpdateAsync(c=> c.SetProperty(p=> p.Deleted , true ).
                                       SetProperty(p => p.UpdatedDate, DateTime.UtcNow),
                                                                      cancellationToken);  

            if (tasks is null) return RequestResult<bool>.Failure(ErrorCode.InvalidInput, "there is no tasks to that project");
        
      
          await  _repository.SaveChangesAsync();
            return RequestResult<bool>.Success(true, "every task deleted relatedd to that project ");
        }
    }


}
