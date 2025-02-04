using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using TrelloCopy.Common.BaseHandlers;
using TrelloCopy.Common.Data.Enums;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.Common.Command;
using TrelloCopy.Features.Common.Projects.Queries;
using TrelloCopy.Models;

namespace TrelloCopy.Features.ProjectManage.DeleteProject.Command
{
    public record DeleteProjectCommand(int ProjectId) : IRequest<RequestResult<bool>>;

    public class DeleteProjectCommandHandler : BaseRequestHandler<DeleteProjectCommand, RequestResult<bool>, Project>
    {
        public DeleteProjectCommandHandler(BaseWithoutRepositoryRequestHandlerParameter<Project> parameters) : base(parameters)
        {
        }

        public override async Task<RequestResult<bool>> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _repository.Get(c => c.ID == request.ProjectId&&!c.Deleted).FirstOrDefaultAsync(); 

            if (project == null) return RequestResult<bool>.Failure(ErrorCode.ProjectNotFound, "Project not found");
                var deleteTasksResult = await _mediator.Send(new DeleteTasksRelatedToProject(project.ID));
                if (!deleteTasksResult.isSuccess)
                    return RequestResult<bool>.Failure(deleteTasksResult.errorCode, deleteTasksResult.message);

            //foreach (var item in project.SprintItems)
            //{

            //    item.Deleted = true;

            //}

            
            //await _repository.SaveIncludeAsync(project , nameof(project.SprintItems));
            await _repository.Delete(project);
            await _repository.SaveChangesAsync();
            return RequestResult<bool>.Success(true, "project deleted succssfully ");
        }
    }

}
