using FluentValidation;

namespace TrelloCopy.Features.TaskManagement.Tasks.DeleteTask
{
    public record RequestParames(int id);
    public class RequestParamesValidator : AbstractValidator<RequestParames>
    {
        public RequestParamesValidator()
        {

        }
    }
}