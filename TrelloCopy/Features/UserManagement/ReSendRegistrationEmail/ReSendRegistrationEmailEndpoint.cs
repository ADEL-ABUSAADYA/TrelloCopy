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
      var isReSend = await _mediator.Send(reSendCommand);
      if (!isReSend.isSuccess)
         return EndpointResponse<bool>.Failure(isReSend.errorCode, isReSend.message);
      
      return EndpointResponse<bool>.Success(true);
   }
}
