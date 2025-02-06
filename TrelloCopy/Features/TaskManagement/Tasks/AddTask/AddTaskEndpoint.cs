using Microsoft.AspNetCore.Mvc;
using TrelloCopy.Common.BaseEndpoints;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.TaskManagement.Tasks.AddTask.Command;

namespace TrelloCopy.Features.TaskManagement.Tasks.AddTask
{
    
    public class AddTaskEndpoint : BaseEndpoint<AddTaskRequestViewModel, bool>
    {

        public AddTaskEndpoint(BaseEndpointParameters<AddTaskRequestViewModel> parameters) : base(parameters)
        {
        }
        
        [HttpPost("api/AddTask")]
        public async Task<EndpointResponse<bool>> AddTask(AddTaskRequestViewModel viewmodel)
        {
            var validationResult = ValidateRequest(viewmodel);
            if (!validationResult.isSuccess)
                return validationResult;

            var task = await _mediator.Send(new AddTaskCommand(viewmodel.Title, viewmodel.Description, Models.Enums.TaskStatus.ToDo.ToString(), viewmodel.UserId, viewmodel.ProjectId));
            if (!task.isSuccess)
                return EndpointResponse<bool>.Failure(task.errorCode, task.message);

            return EndpointResponse<bool>.Success(true);
        }
    }
}
