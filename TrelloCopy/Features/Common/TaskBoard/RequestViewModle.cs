using FluentValidation;

namespace TrelloCopy.Features.Common.TaskBoard
{
    public record RequestViewModle(int id ); 
    
        public class RequestViewModleValidator : AbstractValidator<RequestViewModle>
        {



        }
    }

