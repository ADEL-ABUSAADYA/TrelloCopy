using Microsoft.AspNetCore.Mvc;
using TrelloCopy.Common.BaseEndpoints;
using TrelloCopy.Common.Helper;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.ProjectManage.GetAllProject.Query;

namespace TrelloCopy.Features.ProjectManage.GetAllProject
{
    public class GetAllProjectEndPoint : BaseEndpoint<ProjectParames, ProjectResponseViewModel>
    {
        public GetAllProjectEndPoint(BaseEndpointParameters<ProjectParames> parameters) : base(parameters)
        {
        }

        [HttpGet("FilterProjec")]
        public async Task<EndpointResponse<ProjectResponseViewModel>> GetAllPeoject([FromQuery] ProjectParames parames)
        {
            var validationResult = ValidateRequest(parames);
            if (!validationResult.isSuccess)
                return validationResult;

            var FilteredProject = await _mediator.Send(new GetAllProjectsQuery(parames.PageNumber, parames.PageSize, parames.title));

            if (!FilteredProject.isSuccess)
                return EndpointResponse<ProjectResponseViewModel>.Failure(FilteredProject.errorCode, FilteredProject.message);
            var response = new ProjectResponseViewModel
            {
                Projects = FilteredProject.data.Select(U => new GetProjectDTo
                {
                    title = U.title,
                    description = U.description,
                    NumTask = U.NumTask,
                    NumUSers = U.NumUSers,

                }).ToList(),
                PageNumber = parames.PageNumber,
                PageSize = parames.PageSize,
                totalNumber = FilteredProject.data.TotalCount
            };
            return EndpointResponse<ProjectResponseViewModel>.Success(response);
        }


    }
}
