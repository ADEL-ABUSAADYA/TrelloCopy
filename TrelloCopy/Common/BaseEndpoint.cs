using System.Security.Claims;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TrelloCopy.Common.Data.Enums;
using TrelloCopy.Common.Views;

namespace TrelloCopy.Common;

[ApiController]
[Route("[controller]/[action]")]
//[ServiceFilter(typeof(CustomeAuthorizeFilter))]
public class BaseEndpoint<TRequest, TResponse> : ControllerBase
{
    protected IMediator _mediator;
    protected IValidator<TRequest> _validator;
    protected UserInfo _userInfo;

    public BaseEndpoint(BaseEndpointParameters<TRequest> parameters)
    {
        _mediator = parameters.Mediator;
        _validator = parameters.Validator;
        
        _userInfo = parameters.UserInfo;
    }

    protected EndpointResponse<TResponse> ValidateRequest(TRequest request)
    {
        var validationResult = _validator.Validate(request);

        if (!validationResult.IsValid)
        {
            var validationError = string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage));

            return EndpointResponse<TResponse>.Failure(ErrorCode.InvalidInput, validationError);
        }

        return EndpointResponse<TResponse>.Success(default);
    }
}
// var userClaims = _httpContextAccessor.HttpContext?.User?.Claims;
//
// if (userClaims != null)
// {
//     // Try to parse the NameIdentifier to an integer
//     var userId = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
//     int parsedUserId = 0;
//     if (!string.IsNullOrEmpty(userId) && !int.TryParse(userId, out parsedUserId))
//     {
//         // Handle the case where the ID could not be parsed
//         throw new Exception("Invalid User ID in claims");
//     }
//
//     UserInfo = new UserInfo
//     {
//         ID = parsedUserId, // Set the parsed ID
//         Name = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value,
//         Email = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value
//     };
// }
