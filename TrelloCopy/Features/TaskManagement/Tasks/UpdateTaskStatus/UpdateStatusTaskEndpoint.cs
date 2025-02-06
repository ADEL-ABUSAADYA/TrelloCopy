using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrelloCopy.Common.BaseEndpoints;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.TaskManagement.Tasks.UpdateTaskStatus.Commands;

namespace TrelloCopy.Features.TaskManagement.Tasks.UpdateTaskStatus
{
    public class UpdateStatusTaskEndPoint : BaseEndpoint<RequsetUpdateTaskModel, bool>
    {
        public UpdateStatusTaskEndPoint(BaseEndpointParameters<RequsetUpdateTaskModel> parameters) : base(parameters)
        {
        }

        [HttpPut]
        [Authorize]
        public async Task<EndpointResponse<bool>> ChangeTaskStatus( RequsetUpdateTaskModel model)
        {
            var validationResult = ValidateRequest(model);
            if (!validationResult.isSuccess)
                return validationResult;

            var responose = await _mediator.Send(new UpdateStatusCommand(model.id , model.Status));
            
            if(!responose.isSuccess) return EndpointResponse<bool>.Failure(responose.errorCode ,responose.message);

            return EndpointResponse<bool>.Success(true ,"status changed sucssfuly");
        }
    }
}
