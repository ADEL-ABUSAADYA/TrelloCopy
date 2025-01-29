using Azure;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using TrelloCopy.Common;
using TrelloCopy.Common.Data.Enums;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.UserManagement.ResetPassword.Commands;

namespace TrelloCopy.Features.UserManagement.ResetPassword
{
    public class ResetPasswordEndPoint : BaseEndpoint<ResetPasswordRequestViewModel, IActionResult>
    {
        public ResetPasswordEndPoint(BaseEndpointParameters<ResetPasswordRequestViewModel> parameters) : base(parameters)
        {
        }


        [HttpPost]
         
        public async Task<EndpointResponse<IActionResult>> ResetPassword(ResetPasswordRequestViewModel parameters)
        {
            var validateInput = ValidateRequest(parameters);
            if (!validateInput.isSuccess) return EndpointResponse<IActionResult>.Failure(ErrorCode.InvalidInput);

            var response =await  _mediator.Send(new ResetPasswordCommand(parameters.otp, parameters.Email, parameters.Email, parameters.ConfirmPassword));

            if (!response.isSuccess) return EndpointResponse<IActionResult>.Failure(response.errorCode, response.message); 

            return EndpointResponse<IActionResult>.Success(Ok(response), "password change ");
        }

    }
}
