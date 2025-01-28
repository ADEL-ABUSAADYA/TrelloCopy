using System.Security.Claims;
using FluentValidation;
using MediatR;
using TrelloCopy.Common.Views;

namespace TrelloCopy.Common;
public class BaseEndpointParameters<TRequest>
{
    readonly IMediator _mediator;
    readonly IValidator<TRequest> _validator;
    readonly IHttpContextAccessor _httpContextAccessor;

    public IMediator Mediator => _mediator;
    public IValidator<TRequest> Validator => _validator;
    public UserInfo UserInfo { get; set; }

    public BaseEndpointParameters(IMediator mediator, IValidator<TRequest> validator, IHttpContextAccessor httpContextAccessor)
    {
        _mediator = mediator;
        _validator = validator;
        _httpContextAccessor = httpContextAccessor;

        // Extract UserInfo from claims and set it
        var userClaims = _httpContextAccessor.HttpContext?.User?.Claims;

        if (userClaims != null)
        {
            // Try to parse the NameIdentifier to an integer
            var userId = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            int parsedUserId = 0;
            if (!string.IsNullOrEmpty(userId) && !int.TryParse(userId, out parsedUserId))
            {
                // Handle the case where the ID could not be parsed
                throw new Exception("Invalid User ID in claims");
            }

            UserInfo = new UserInfo
            {
                ID = parsedUserId, // Set the parsed ID
                Name = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value,
                Email = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value
            };
        }
    }
}

