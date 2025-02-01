using Microsoft.AspNetCore.Mvc;
using TrelloCopy.Common.BaseEndpoints;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.AuthManagement.LogInUser;
using TrelloCopy.Features.AuthManagement.LogInUser.Commands;

namespace TrelloCopy.Features.UserManagement.GetAllUsers;

public class GetAllUsersEndpoint : BaseWithoutTRequestEndpoint<UserResponseViewModel>
{
   public GetAllUsersEndpoint(BaseWithoutTRequestEndpointParameters parameters) : base(parameters)
   {
   }

   [HttpPost]
   public async Task<EndpointResponse<LoginResponeViewModel>> LogInUser(LogInUserRequestViewModel viewmodel)
   {
      var loginCommand = new LogInUserCommand(viewmodel.Email, viewmodel.Password);
      var logInToken = await _mediator.Send(loginCommand);
      if (!logInToken.isSuccess)
         return EndpointResponse<LoginResponeViewModel>.Failure(logInToken.errorCode, logInToken.message);
      
      return EndpointResponse<LoginResponeViewModel>.Success(new LoginResponeViewModel(Token: logInToken.data.LogInToken, TokenWith2FA: logInToken.data.TokenWith2FA));
   }
}
