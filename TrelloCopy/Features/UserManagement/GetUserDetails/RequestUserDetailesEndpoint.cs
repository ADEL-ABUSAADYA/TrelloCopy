using FluentValidation;
using TrelloCopy.Features.UserManagement.GetAllUsers;

namespace TrelloCopy.Features.UserManagement.GetUserDetalies
{
    public record RequestUserDetailesEndpoint(int id);

    public class RequestUserDetailesEndpointValidator : AbstractValidator<RequestUserDetailesEndpoint>
    {
        public RequestUserDetailesEndpointValidator()
        {
          
        }
    }


}