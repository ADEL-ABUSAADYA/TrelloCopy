using FluentValidation;
using TrelloCopy.Common.Helper;

namespace TrelloCopy.Features.TaskManagement.Tasks.GetAllTasks
{
    public  class GetFilteredTasksParams (Dictionary<string, object> filters): QueryStringParamater
    {
        public Dictionary<string, object> filters { get; set; } = new();
    }
    public class GetFilteredTasksParamsValidator : AbstractValidator<GetFilteredTasksParams>
    {
        public GetFilteredTasksParamsValidator()
        {

        }
    }
}
