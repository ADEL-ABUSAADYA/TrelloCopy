using Microsoft.AspNetCore.Mvc;
using TrelloCopy.Common.BaseEndpoints;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.AuthManagement.ChangePassword.Commands;
using TrelloCopy.Features.AuthManagement.ChangePassword;
using Microsoft.AspNetCore.Authorization;


namespace TrelloCopy.Features.AuthManagement.ChangePassword;

public class ChangePasswordEndPoint : BaseEndpoint<ChangePasswordRequestViewModel, bool>
{
    public ChangePasswordEndPoint(BaseEndpointParameters<ChangePasswordRequestViewModel> parameters) : base(parameters)
    {
    }

    [HttpPut]
    [Authorize]
    public async Task<EndpointResponse<bool>> ChangePassword(ChangePasswordRequestViewModel viewModel)
    {
        var validationResult = ValidateRequest(viewModel);
        if (!validationResult.isSuccess)
            return validationResult;

        var changePasswordCommand = new ChangePasswordCommand(viewModel.CurrentPassword, viewModel.NewPassword);
        var IsChanged = await _mediator.Send(changePasswordCommand);
        if (!IsChanged.isSuccess)
            return EndpointResponse<bool>.Failure(IsChanged.errorCode, IsChanged.message);
        return EndpointResponse<bool>.Success(true,"changed sucssfully");



    }

}
