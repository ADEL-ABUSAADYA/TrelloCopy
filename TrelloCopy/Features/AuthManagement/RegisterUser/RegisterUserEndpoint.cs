using Microsoft.AspNetCore.Mvc;
using TrelloCopy.Common.BaseEndpoints;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.AuthManagement.RegisterUser.Commands;

namespace TrelloCopy.Features.AuthManagement.RegisterUser;

public class RegisterUserEndpoint : BaseEndpoint<RegisterUserRequestViewModel, bool>
{
   public RegisterUserEndpoint(BaseEndpointParameters<RegisterUserRequestViewModel> parameters) : base(parameters)
   {
   }

   [HttpPut]
   public async Task<EndpointResponse<bool>> RegisterUser(RegisterUserRequestViewModel viewmodel)
   {
      var validationResult =  ValidateRequest(viewmodel);
      if (!validationResult.isSuccess)
         return validationResult;
      
      var regisetrCommand = new RegisterUserCommand(viewmodel.Email, viewmodel.Password, viewmodel.Name, viewmodel.PhoneNo, viewmodel.Country);
      var isRegistered = await _mediator.Send(regisetrCommand);
      if (!isRegistered.isSuccess)
         return EndpointResponse<bool>.Failure(isRegistered.errorCode, isRegistered.message);
      
      return EndpointResponse<bool>.Success(true,"ples confirm your email");
   }
}
