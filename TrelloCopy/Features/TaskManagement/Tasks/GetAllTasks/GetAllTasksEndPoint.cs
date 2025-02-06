using Microsoft.AspNetCore.Mvc;
using TrelloCopy.Common.BaseEndpoints;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.TaskManagement.Tasks.GetAllTasks.Query;

namespace TrelloCopy.Features.TaskManagement.Tasks.GetAllTasks
{
    public class GetAllTasksEndPoint : BaseEndpoint<GetAllTasksParams, GetAllTasksResponseViewModel>
    {
        public GetAllTasksEndPoint(BaseEndpointParameters<GetAllTasksParams> parameters) : base(parameters)
        {
        }
        [HttpGet]
        public async Task<EndpointResponse<GetAllTasksResponseViewModel>> GetAllTasks([FromQuery] GetAllTasksParams parametres)
        {
            var validationResult = ValidateRequest(parametres);
            if (!validationResult.isSuccess)
                return validationResult;
            var requestResponse = await _mediator.Send(new GetAllTasksQuery(parametres.PageNumber, parametres.PageSize , parametres.title));
            if (!requestResponse.isSuccess)
                return EndpointResponse<GetAllTasksResponseViewModel>.Failure(requestResponse.errorCode, requestResponse.message);
            var response = new GetAllTasksResponseViewModel()
            {
                Tasks = requestResponse.data.Select(t => new GetTaskDto(t.Title, t.Description, t.User, t.Status, t.Project, t.CreatedDate)).ToList(),
                PageNumber = parametres.PageNumber,
                PageSize = parametres.PageSize,
                TotalNumber = requestResponse.data.TotalCount
            };

            return EndpointResponse<GetAllTasksResponseViewModel>.Success(response);
        }
    }
}
