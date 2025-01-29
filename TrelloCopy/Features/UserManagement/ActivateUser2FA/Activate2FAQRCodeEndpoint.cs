using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtpNet;
using TrelloCopy.Common;
using TrelloCopy.Common.Data.Enums;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.UserManagement.ActivateUser2FA.Commands;
using TrelloCopy.Features.UserManagement.LogInUser;
using TrelloCopy.Filters;

namespace TrelloCopy.Features.UserManagement.ActivateUser2FA;

public class Activate2FAQRCodeEndpoint : BaseEndpoint<LogInInfoDTO, string>
{
    public Activate2FAQRCodeEndpoint(BaseEndpointParameters<LogInInfoDTO> parameters) : base(parameters)
    {
    }
    
    [HttpPost]
    [Authorize]
    [TypeFilter(typeof(CustomizeAuthorizeAttribute), Arguments =new object[] {Feature.ActivateUser2FA})]
    public async Task<EndpointResponse<string>> ActivateUser2FA()
    {
        var activateCommand = await _mediator.Send(new ActivateUser2FAOrchestrator());
        if (!activateCommand.isSuccess)
            return EndpointResponse<string>.Failure(activateCommand.errorCode, activateCommand.message);
        
        return EndpointResponse<string>.Success(activateCommand.data);
    }
}