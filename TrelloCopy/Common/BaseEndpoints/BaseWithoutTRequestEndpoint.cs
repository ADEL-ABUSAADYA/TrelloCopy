using System.Security.Claims;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrelloCopy.Common.Data.Enums;
using TrelloCopy.Common.Views;
using TrelloCopy.Filters;

namespace TrelloCopy.Common.BaseEndpoints;

[ApiController]
[Route("[controller]/[action]")]

// [TypeFilter(typeof(UserInfoFilter))]
public class BaseWithoutTRequestEndpoint<TResponse> : ControllerBase
{
    protected IMediator _mediator;
    protected UserInfo _userInfo;

    public BaseWithoutTRequestEndpoint(BaseWithoutTRequestEndpointParameters parameters)
    {
        _mediator = parameters.Mediator;
        _userInfo = parameters.UserInfo;
    }

}
