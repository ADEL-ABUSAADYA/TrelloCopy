using MediatR;
using TrelloCopy.Common.Views;

namespace TrelloCopy.Common.BaseEndpoints;
public class BaseWithoutTRequestEndpointParameters
{
    readonly IMediator _mediator;
    readonly UserInfo _userInfo;
    
    public IMediator Mediator => _mediator;

    public UserInfo UserInfo => _userInfo;
   
    public BaseWithoutTRequestEndpointParameters(IMediator mediator, UserInfoProvider userInfoProvider)
    {
        _mediator = mediator;
        _userInfo = userInfoProvider.UserInfo;
    }
}

