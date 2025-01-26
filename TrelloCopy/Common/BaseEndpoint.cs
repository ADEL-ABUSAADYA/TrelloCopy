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

        parameters.UserInfo = new UserInfo { };
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

