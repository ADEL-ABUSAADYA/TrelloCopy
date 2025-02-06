using Microsoft.AspNetCore.Mvc;
using TrelloCopy.Common.BaseEndpoints;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.TaskManagement.Tasks.GetAllTasks.Query;
using TrelloCopy.Features.TaskManagement.Tasks.GetTaskDetails.Query;

namespace TrelloCopy.Features.TaskManagement.Tasks.GetTaskDetails
{
    public class GetTaskDetailsEndPoint : BaseEndpoint<RequestParame, GetTaskDto>
    {
        public GetTaskDetailsEndPoint(BaseEndpointParameters<RequestParame> parameters) : base(parameters)
        {
        }

        [HttpGet]
        public async Task<EndpointResponse<GetTaskDto>> GetTaskDetails([FromQuery]RequestParame request)
        {
            var vailtion = ValidateRequest(request);
            if (!vailtion.isSuccess) return vailtion;

            var requestResponse = await _mediator.Send(new GetTaskDetailsQuery(request.id));
            if (!requestResponse.isSuccess)
                return EndpointResponse<GetTaskDto>.Failure(requestResponse.errorCode, requestResponse.message);
            return EndpointResponse<GetTaskDto>.Success(requestResponse.data);
        }   
    }
}
