using FluentValidation;
using TrelloCopy.Features.UserManagement.GetAllUsers;

namespace TrelloCopy.Features.UserManagement.GetUserDetalies
{
    public record RequestUserActivtaionStatus(int id);

    public class RequestUserActivtaionStatusValidator : AbstractValidator<RequestUserActivtaionStatus>
    {
        public RequestUserActivtaionStatusValidator()
        {
          
        }
    }


}