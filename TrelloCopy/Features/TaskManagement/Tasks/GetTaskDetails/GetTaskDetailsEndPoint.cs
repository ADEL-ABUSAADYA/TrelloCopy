using Microsoft.AspNetCore.Mvc;
using TrelloCopy.Common.BaseEndpoints;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.TaskManagement.Tasks.GetAllTasks.Query;
using TrelloCopy.Features.TaskManagement.Tasks.GetTaskDetails.Query;

namespace TrelloCopy.Features.TaskManagement.Tasks.GetTaskDetails
{
    public class GetTaskDetailsEndPoint : BaseEndpoint<int, GetTaskDto>
    {
        public GetTaskDetailsEndPoint(BaseEndpointParameters<int> parameters) : base(parameters)
        {
        }

        [HttpGet("{id:int}")]
        public async Task<EndpointResponse<GetTaskDto>> GetTaskDetails(int id)
        {
            var requestResponse = await _mediator.Send(new GetTaskDetailsQuery(id));
            if (!requestResponse.isSuccess)
                return EndpointResponse<GetTaskDto>.Failure(requestResponse.errorCode, requestResponse.message);
            return EndpointResponse<GetTaskDto>.Success(requestResponse.data);
        }   
    }
}
