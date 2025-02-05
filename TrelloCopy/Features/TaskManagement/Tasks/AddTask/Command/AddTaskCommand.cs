using Azure;
using MediatR;
using TrelloCopy.Common.BaseHandlers;
using TrelloCopy.Common.Views;
using TrelloCopy.Models;


namespace TrelloCopy.Features.TaskManagement.Tasks.AddTask.Command
{
    public record AddTaskCommand(string Title , string Description , string Status , int UserId , int ProjectId ) :IRequest<RequestResult<bool>>
    {
    }
    public class AddTaskCommandHandler : BaseRequestHandler<AddTaskCommand, RequestResult<bool>, TaskEntity>
    {
        public AddTaskCommandHandler(BaseWithoutRepositoryRequestHandlerParameter<TaskEntity> parameters) : base(parameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(AddTaskCommand request, CancellationToken cancellationToken)
        {
            var task = new TaskEntity
            {
                Title = request.Title,
                Description = request.Description,
                Status = (Models.Enums.TaskStatus)Enum.Parse(typeof(Models.Enums.TaskStatus), request.Status),
                UserId = request.UserId,
                ProjectId = request.ProjectId
            };
            await _repository.AddAsync(task);
            await _repository.SaveChangesAsync();
            return RequestResult<bool>.Success(true, "Task Added Successfully");

        }

    }
}
