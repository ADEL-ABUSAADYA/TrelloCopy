using MediatR;
using TrelloCopy.Common.BaseHandlers;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.Common.Task.Queries;
using TrelloCopy.Models;

namespace TrelloCopy.Features.TaskManagement.Tasks.DeleteTask.Command
{
    public record DeleteTaskCommand(int Id) : IRequest<RequestResult<bool>>;
    public class DeleteTaskCommandHandler : BaseRequestHandler<DeleteTaskCommand, RequestResult<bool>, TaskEntity>
    {
        public DeleteTaskCommandHandler(BaseWithoutRepositoryRequestHandlerParameter<TaskEntity> parameters) : base(parameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var isTaskExist = await _mediator.Send(new IsTaskExist(request.Id));
            if (!isTaskExist.isSuccess)
            {
                return RequestResult<bool>.Failure(isTaskExist.errorCode, isTaskExist.message);
            }
            var task = new TaskEntity
            {
                ID = request.Id,
                Deleted = true
            };
          await  _repository.SaveIncludeAsync(task,nameof(task.Deleted));
           await _repository.SaveChangesAsync();
            return RequestResult<bool>.Success(true,"Task Deleted Successfully");
        }
    }
}
