using Microsoft.AspNetCore.Mvc;
using TrelloCopy.Common;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.UserManagement.ChangePassword.Commands;
using TrelloCopy.Features.UserManagement.ChangePassword;


namespace TrelloCopy.Features.UserManagement.ChangePassword;

public class ChangePasswordEndPoint : BaseEndpoint<ChangePasswordRequestViewModel, bool>
{
    public ChangePasswordEndPoint(BaseEndpointParameters<ChangePasswordRequestViewModel> parameters) : base(parameters)
    {
    }

    [HttpPut]
    public async Task<EndpointResponse<bool>> ChangePassword(ChangePasswordRequestViewModel viewModel)
    {
        var validationResult = ValidateRequest(viewModel);
        if (!validationResult.isSuccess)
            return validationResult;

        var changePasswordCommand = new ChangePasswordCommand(viewModel.CurrentPassword, viewModel.NewPassword);
        var IsChanged = await _mediator.Send(changePasswordCommand);
        if (!IsChanged.isSuccess)
            return EndpointResponse<bool>.Failure(IsChanged.errorCode, IsChanged.message);
        return EndpointResponse<bool>.Success(true);



    }

}
