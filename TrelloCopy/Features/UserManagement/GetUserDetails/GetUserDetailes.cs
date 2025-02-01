using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using TrelloCopy.Common;
using TrelloCopy.Common.BaseEndpoints;
using TrelloCopy.Common.Data.Enums;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.UserManagement.GetUserDetalies;
using TrelloCopy.Features.UserManagement.GetUserDetalies.Queary;


namespace TrelloCopy.Features.UserManagement.GetUserDetalies
{
  
    public class UserDetaileEndpoint : BaseEndpoint<RequestUserDetailesEndpoint ,ResponseUserDetailsEndpoint >
    {
        public UserDetaileEndpoint(BaseEndpointParameters<RequestUserDetailesEndpoint> parameters) : base(parameters)
        {
        }

        [HttpGet]
        public async Task<EndpointResponse<ResponseUserDetailsEndpoint>> GetUSerDetails([FromQuery] RequestUserDetailesEndpoint request)
        {
            var validationResult = ValidateRequest(request);
            if (!validationResult.isSuccess)
                return EndpointResponse<ResponseUserDetailsEndpoint>.Failure(ErrorCode.InvalidInput);
            
            var response = await _mediator.Send(new GetUserDetailesQuery(request.id));



            if (!response.isSuccess)
                return EndpointResponse<ResponseUserDetailsEndpoint>.Failure(response.errorCode , response.message);

            

            var responsee = new ResponseUserDetailsEndpoint
            {
                email = response.data.email,
                IsActive = response.data.IsActive,
                name = response.data.name,
                PhoneNo = response.data.PhoneNo
            };

            return EndpointResponse<ResponseUserDetailsEndpoint>.Success(responsee);

        }


    }
}
