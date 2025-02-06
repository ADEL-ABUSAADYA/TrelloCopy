using Microsoft.AspNetCore.Mvc;
using System.Net;
using TrelloCopy.Common.BaseEndpoints;
using TrelloCopy.Common.Data.Enums;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.TaskManagement.Tasks.DeleteTask.Command;

namespace TrelloCopy.Features.TaskManagement.Tasks.DeleteTask
{
    public class DeleteTaskEndpoint(BaseEndpointParameters<RequestParames> parameters) : BaseEndpoint<RequestParames, bool>(parameters)
    {
        [HttpDelete]
        public async Task<EndpointResponse<bool>> DeleteTask([FromQuery] RequestParames request)
        {
            var vailtion = ValidateRequest(request);
            if (!vailtion.isSuccess) return vailtion;

            var result = await _mediator.Send(new DeleteTaskCommand(request.id));
            return result.isSuccess ? EndpointResponse<bool>.Success(result.data, result.message) 
                : EndpointResponse<bool>.Failure(result.errorCode, result.message);
        }
    }

}
