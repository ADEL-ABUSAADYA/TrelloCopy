using Microsoft.AspNetCore.Mvc;
using TrelloCopy.Common.BaseEndpoints;
using TrelloCopy.Common.Data.Enums;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.ProjectManage.DeleteProject.Command;

namespace TrelloCopy.Features.ProjectManage.DeleteProject
{
    public class DeleteProjectEndpoint : BaseEndpoint<RequestEndPointModel, bool>
    {
        public DeleteProjectEndpoint(BaseEndpointParameters<RequestEndPointModel> parameters) : base(parameters)
        {
        }

        [HttpDelete]
        public async Task<EndpointResponse<bool>> DeletProject([FromQuery]RequestEndPointModel request)
        {
            var vailtion = ValidateRequest(request);
            if (!vailtion.isSuccess) return vailtion; 

            var response = await _mediator.Send(new DeleteProjectCommand(request.id)); 

            if(!response.isSuccess) return EndpointResponse<bool>.Failure(response.errorCode,response.message);

            return EndpointResponse<bool>.Success(true, "project is deleted");

        }
    }
}
