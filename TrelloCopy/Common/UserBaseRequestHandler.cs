using MediatR;
using TrelloCopy.Common.Views;
using TrelloCopy.Data.Repositories;
using TrelloCopy.Helpers;
using TrelloCopy.Models;

namespace TrelloCopy.Common;

public abstract class UserBaseRequestHandler<TRequest, TRespone> : IRequestHandler<TRequest, TRespone> where TRequest : IRequest<TRespone>
{
    protected readonly IMediator _mediator;
    protected readonly IUserRepository _userRepository;
    protected readonly TokenHelper _tokenHelper;
    public UserInfo _userInfo { get; set; }

    public UserBaseRequestHandler(UserBaseRequestHandlerParameters parameters)
    {
        _mediator = parameters.Mediator;
        _userRepository = parameters.UserRepository;
        _tokenHelper = parameters.TokenHelper;
        _userInfo = parameters.UserInfo;
    }
    public abstract Task<TRespone> Handle(TRequest request, CancellationToken cancellationToken);
}

