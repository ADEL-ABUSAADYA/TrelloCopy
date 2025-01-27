using Microsoft.AspNetCore.Mvc;
using TrelloCopy.Common;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.userManagement.RegisterUser.Commands;
using TrelloCopy.Features.UserManagement.ReSendRegistrationEmail.Commands;

namespace TrelloCopy.Features.UserManagement.ResendRegistrationEmail;

public class ReSendRegistrationEmailEndpoint : BaseEndpoint<ReSendRegistrationEmailRequestViewModel, bool>
{
   public ReSendRegistrationEmailEndpoint(BaseEndpointParameters<ReSendRegistrationEmailRequestViewModel> parameters) : base(parameters)
   {
   }

   [HttpPut]
   public async Task<EndpointResponse<bool>> ReSendEmail(ReSendRegistrationEmailRequestViewModel viewmodel)
   {
      var validationResult =  ValidateRequest(viewmodel);
      if (!validationResult.isSuccess)
         return validationResult;
      
      var reSendCommand = new ReSendRegistrationEmailCommand(viewmodel.Email);
      var isReSent = await _mediator.Send(reSendCommand);
      if (!isReSent.isSuccess)
         return EndpointResponse<bool>.Failure(isReSent.errorCode, isReSent.message);
      
      return EndpointResponse<bool>.Success(true);
   }
}
