using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using TrelloCopy.Common;
using TrelloCopy.Common.BaseEndpoints;
using TrelloCopy.Common.Data.Enums;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.UserManagement.BlockUser.Command;
using TrelloCopy.Features.UserManagement.GetUserDetalies;
using TrelloCopy.Features.UserManagement.GetUserDetalies.Queary;


namespace TrelloCopy.Features.UserManagement.GetUserDetalies
{
  
    public class BlockUserEndPoint : BaseEndpoint<RequestUserActivtaionStatus,bool >
    {
        public BlockUserEndPoint(BaseEndpointParameters<RequestUserActivtaionStatus> parameters) : base(parameters)
        {
        }

        [HttpPut]
        public async Task<EndpointResponse<bool>> GetUSerDetails([FromQuery] RequestUserActivtaionStatus request)
        {
            var validationResult = ValidateRequest(request);
            if (!validationResult.isSuccess)
                return EndpointResponse<bool>.Failure(ErrorCode.InvalidInput);
            
            var response = await _mediator.Send(new ChangeStatusCommand(request.id));

            if (!response.isSuccess)
                return EndpointResponse<bool>.Failure(response.errorCode , response.message);


            return EndpointResponse<bool>.Success(response.isSuccess, "the user has deactivate ");

        }


    }
}
