using Microsoft.AspNetCore.Mvc;
using TrelloCopy.Common;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.userManagement.RegisterUser.Commands;

namespace TrelloCopy.Features.UserManagement.RegisterUser;

public class RegisterUserEndpoint : BaseEndpoint<RegesterUserRequestViewModel, bool>
{
   public RegisterUserEndpoint(BaseEndpointParameters<RegesterUserRequestViewModel> parameters) : base(parameters)
   {
   }

   [HttpPut]
   public async Task<EndpointResponse<bool>> RegisterUser(RegesterUserRequestViewModel viewmodel)
   {
      var validationResult =  ValidateRequest(viewmodel);
      if (!validationResult.isSuccess)
         return validationResult;
      
      var regisetrCommand = new RegisterUserCommand(viewmodel.email, viewmodel.password, viewmodel.name, viewmodel.phoneNo, viewmodel.country);
      var isRegistered = await _mediator.Send(regisetrCommand);
      if (!isRegistered.isSuccess)
         return EndpointResponse<bool>.Failure(isRegistered.errorCode, isRegistered.message);
      
      return EndpointResponse<bool>.Success(true);
   }
}
