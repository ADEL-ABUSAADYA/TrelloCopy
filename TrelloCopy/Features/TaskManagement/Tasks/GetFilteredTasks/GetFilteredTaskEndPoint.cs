using Microsoft.AspNetCore.Mvc;
using TrelloCopy.Common.BaseEndpoints;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.TaskManagement.Tasks.GetAllTasks;
using TrelloCopy.Features.TaskManagement.Tasks.GetAllTasks.Query;
using TrelloCopy.Features.TaskManagement.Tasks.GetTasksByStatus.Query;

namespace TrelloCopy.Features.TaskManagement.Tasks.GetFilteredTasks
{
    public class GetFilteredTaskEndPoint(BaseEndpointParameters<GetFilteredTasksParams> parameters)
        : BaseEndpoint<GetFilteredTasksParams, GetFilteredTasksResponseViewModel>(parameters)
    {
        [HttpGet]
        public async Task<EndpointResponse<GetFilteredTasksResponseViewModel>> GetFilteredTasks(GetFilteredTasksParams parameters )
        {
            var validationResult = ValidateRequest(parameters);
            if (!validationResult.isSuccess)
                return validationResult;
            var requestResponse = await _mediator.Send(new GetFilteredTasksQuery( parameters.PageNumber, parameters.PageSize, parameters.filters));
            if (!requestResponse.isSuccess)
                return EndpointResponse<GetFilteredTasksResponseViewModel>.Failure(requestResponse.errorCode, requestResponse.message);
            var response = new GetFilteredTasksResponseViewModel()
            {
                Tasks = requestResponse.data.Select(t => new GetTaskDto(t.Title, t.Description, t.User, t.Status, t.Project, t.CreatedDate)).ToList(),
                PageNumber = parameters.PageNumber,
                PageSize = parameters.PageSize,
                TotalNumber = requestResponse.data.Count()  
            };

            return EndpointResponse<GetFilteredTasksResponseViewModel>.Success(response);
        }
    }
}
