using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Bcpg.Sig;
using TrelloCopy.Common.BaseEndpoints;
using TrelloCopy.Common.Data.Enums;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.AuthManagement.RegisterUser.Commands;
using TrelloCopy.Features.ProjectManage.AddProject.Command;
using TrelloCopy.Filters;
using TrelloCopy.Models;

namespace TrelloCopy.Features.ProjectManage.AddProject
{
    public class AddProjectEndPoint : BaseEndpoint<RequestAddProjectModel, bool>
    {
        public AddProjectEndPoint(BaseEndpointParameters<RequestAddProjectModel> parameters) : base(parameters)
        {
        }

        [HttpPost]
        [Authorize]
     
        public async Task<EndpointResponse<bool>> AddBook(RequestAddProjectModel viewmodel)
        {
            var validationResult = ValidateRequest(viewmodel);
            if (!validationResult.isSuccess)
                return validationResult;

            var Project = await _mediator.Send(new AddProjectCommand (viewmodel.Title , viewmodel.Descrbition));
            if (!Project.isSuccess)
                return EndpointResponse<bool>.Failure(Project.errorCode, Project.message);

            return EndpointResponse<bool>.Success(true);
        }


    }
}
