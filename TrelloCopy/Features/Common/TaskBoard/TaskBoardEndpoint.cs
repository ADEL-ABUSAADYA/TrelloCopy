using Microsoft.AspNetCore.Mvc;
using TrelloCopy.Common.BaseEndpoints;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.TaskManagement.Tasks.GetAllTasks.Query;
using TrelloCopy.Features.TaskManagement.Tasks.GetAllTasks;
using TrelloCopy.Features.Common.TaskBoard.Query;

namespace TrelloCopy.Features.Common.TaskBoard
{
    public class TaskBoardEndpoint : BaseEndpoint<RequestViewModle, TaskBoardResponseModel>
    {
        public TaskBoardEndpoint(BaseEndpointParameters<RequestViewModle> parameters) : base(parameters)
        {
      
        }


        [HttpGet]
        public async Task<EndpointResponse<TaskBoardResponseModel>> Taskboard([FromQuery]RequestViewModle request)
        {


            var validationResult = ValidateRequest(request);
            if (!validationResult.isSuccess)
                return validationResult;
            var requestResponse = await _mediator.Send(new TaskBoardQuery(request.id));
            if (!requestResponse.isSuccess)
                return EndpointResponse<TaskBoardResponseModel>.Failure(requestResponse.errorCode, requestResponse.message);
            var response = new TaskBoardResponseModel()
            {
                ToDO = requestResponse.data.ToDO,
                InProgress = requestResponse.data.InProgress,
                Done = requestResponse.data.Done,


            };

            return EndpointResponse<TaskBoardResponseModel>.Success(response);



        }
        


    }
}
