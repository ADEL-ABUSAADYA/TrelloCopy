using Microsoft.AspNetCore.Mvc;
using TrelloCopy.Common;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.userManagement.RegisterUser.Commands;

namespace TrelloCopy.Features.UserManagement.RegisterUser;

public class RegisterUserEndpoint : BaseEndpoint<RegisterUserRequestViewModel, IActionResult>
{
   public RegisterUserEndpoint(BaseEndpointParameters<RegisterUserRequestViewModel> parameters) : base(parameters)
   {
   }

   [HttpPut]
   public async Task<EndpointResponse<IActionResult>> RegisterUser(RegisterUserRequestViewModel viewmodel)
   {
      var validationResult =  ValidateRequest(viewmodel);
      if (!validationResult.isSuccess)
         return validationResult;
      
      var regisetrCommand = new RegisterUserCommand(viewmodel.Email, viewmodel.Password, viewmodel.Name, viewmodel.PhoneNo, viewmodel.Country);
      var isRegistered = await _mediator.Send(regisetrCommand);
      if (!isRegistered.isSuccess)
         return EndpointResponse<IActionResult>.Failure(isRegistered.errorCode, isRegistered.message);
      
      return EndpointResponse<IActionResult>.Success(Ok(regisetrCommand) ,"check your email");
   }
}
