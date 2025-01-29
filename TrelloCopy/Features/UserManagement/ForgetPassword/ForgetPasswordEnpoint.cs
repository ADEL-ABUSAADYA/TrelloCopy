using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrelloCopy.Common;
using TrelloCopy.Common.Data.Enums;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.UserManagement.ForgetPassword.Queries;
using TrelloCopy.Features.UserManagement.RegisterUser;

namespace TrelloCopy.Features.UserManagement.ForgetPassword
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForgetPasswordEnpoint : BaseEndpoint<ForgetPasswordViewModel, IActionResult>
    {
        public ForgetPasswordEnpoint(BaseEndpointParameters<ForgetPasswordViewModel> parameters) : base(parameters)
        {
        }

        [HttpGet]
        public async Task<EndpointResponse<IActionResult>> FrogetPassord( [FromQuery]ForgetPasswordViewModel model)
        {
            var validationResult = ValidateRequest(model);
            if (!validationResult.isSuccess)
                return EndpointResponse<IActionResult>.Failure(ErrorCode.InvalidInput);
            
            var response = await _mediator.Send(new ForgetPasswordQuary(model.email));

            if(!response.isSuccess)
                return EndpointResponse<IActionResult>.Failure(response.errorCode , response.message);

            return EndpointResponse<IActionResult>.Success(Ok(response) , "ples check your email ");

        }


    }
}
