using Microsoft.AspNetCore.Mvc;
using System.Net;
using TrelloCopy.Common.BaseEndpoints;
using TrelloCopy.Common.Data.Enums;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.TaskManagement.Tasks.DeleteTask.Command;

namespace TrelloCopy.Features.TaskManagement.Tasks.DeleteTask
{
    public class DeleteTaskEndpoint(BaseEndpointParameters<int> parameters) : BaseEndpoint<int, bool>(parameters)
    {
        [HttpDelete("/{id:int}")]
        public async Task<EndpointResponse<bool>> DeleteTask(int id)
        {
            var result = await _mediator.Send(new DeleteTaskCommand(id));
            return result.isSuccess ? EndpointResponse<bool>.Success(result.data, result.message) 
                : EndpointResponse<bool>.Failure(result.errorCode, result.message);
        }
    }

}
