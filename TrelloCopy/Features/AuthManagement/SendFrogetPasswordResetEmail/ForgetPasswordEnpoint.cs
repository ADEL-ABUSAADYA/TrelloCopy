using Microsoft.AspNetCore.Mvc;
using TrelloCopy.Common.BaseEndpoints;
using TrelloCopy.Common.Data.Enums;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.AuthManagement.SendFrogetPasswordResetEmail.Commands;

namespace TrelloCopy.Features.AuthManagement.SendFrogetPasswordResetEmail
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendFrogetPasswordResstEmailEnpoint : BaseEndpoint<ForgetPasswordViewModel, bool>
    {
        public SendFrogetPasswordResstEmailEnpoint(BaseEndpointParameters<ForgetPasswordViewModel> parameters) : base(parameters)
        {
        }

        [HttpPost]
        public async Task<EndpointResponse<bool>> SendFrogetPasswordResstEmail( ForgetPasswordViewModel model)
        {
            var validationResult = ValidateRequest(model);
            if (!validationResult.isSuccess)
                return EndpointResponse<bool>.Failure(ErrorCode.InvalidInput);
            
            var response = await _mediator.Send(new SendForgetPasswordResetEmailCommand(model.email));

            if(!response.isSuccess)
                return EndpointResponse<bool>.Failure(response.errorCode , response.message);

            return EndpointResponse<bool>.Success(true);

        }


    }
}
