using Microsoft.AspNetCore.Mvc;
using TrelloCopy.Common.BaseEndpoints;
using TrelloCopy.Common.Helper;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.ProjectManage.GetProjectDetailes.Query;


namespace TrelloCopy.Features.ProjectManage.GetProjectDetailes
{
    public class GetProjectDetailsEndPoint : BaseEndpoint<ProjectDetailsRequest, ProjectDetailsResponseViewModel>
    {
        public GetProjectDetailsEndPoint(BaseEndpointParameters<ProjectDetailsRequest> parameters) : base(parameters)
        {
        }

        [HttpGet("GetProjectDetails")]
        public async Task<EndpointResponse<ProjectDetailsResponseViewModel>> GetProjectDetails([FromQuery] ProjectDetailsRequest request)
        {
            var vaildtaion = ValidateRequest(request);

            if (!vaildtaion.isSuccess) return vaildtaion;



            var result = await _mediator.Send(new GetProjectDetailsQuery(request.id));
            if (!result.isSuccess)
                return EndpointResponse<ProjectDetailsResponseViewModel>.Failure(result.errorCode, result.message);
            var response = new ProjectDetailsResponseViewModel
            {
                title = result.data.title,
                description = result.data.description,
                NumUSers = result.data.NumUSers,
                NumTask = result.data.NumTask,
                CreatedDate = result.data.CreatedDate,
            };



            return EndpointResponse<ProjectDetailsResponseViewModel>.Success(response);
        }
    }
}
