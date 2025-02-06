using FluentValidation;
using TrelloCopy.Common.Helper;

namespace TrelloCopy.Features.TaskManagement.Tasks.GetAllTasks
{
    public class GetAllTasksParams : QueryStringParamater
    {
        public string? title {  get; set; }
    }

    public class GetAllTasksParamsValidator : AbstractValidator<GetAllTasksParams>
    {
        public GetAllTasksParamsValidator()
        {

        }
    }
}
