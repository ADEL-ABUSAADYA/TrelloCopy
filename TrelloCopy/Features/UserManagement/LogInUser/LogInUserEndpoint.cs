using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrelloCopy.Common;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.UserManagement.LogInUser.Commands;
using TrelloCopy.Features.userManagement.RegisterUser.Commands;
using TrelloCopy.Features.UserManagement.ReSendRegistrationEmail.Commands;

namespace TrelloCopy.Features.UserManagement.LogInUser;

public class LogInUserEndpoint : BaseEndpoint<LogInUserRequestViewModel, string>
{
   public LogInUserEndpoint(BaseEndpointParameters<LogInUserRequestViewModel> parameters) : base(parameters)
   {
   }

   [HttpPost]
   public async Task<EndpointResponse<string>> LogInUser(LogInUserRequestViewModel viewmodel)
   {
      var validationResult =  ValidateRequest(viewmodel);
      if (!validationResult.isSuccess)
         return validationResult;
      
      var loginCommand = new LogInUserCommand(viewmodel.Email, viewmodel.Password);
      var logInToken = await _mediator.Send(loginCommand);
      if (!logInToken.isSuccess)
         return EndpointResponse<string>.Failure(logInToken.errorCode, logInToken.message);
      
      return EndpointResponse<string>.Success(logInToken.data);
   }
}
