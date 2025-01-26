using MediatR;
using TrelloCopy.Data.Repositories;
using TrelloCopy.Models;

namespace TrelloCopy.Common;

public abstract class UserBaseRequestHandler<TRequest, TRespone> : IRequestHandler<TRequest, TRespone> where TRequest : IRequest<TRespone>
{
    protected readonly IMediator _mediator;
    protected readonly IUserRepository _userRepository;

    public UserBaseRequestHandler(UserBaseRequestHandlerParameters parameters)
    {
        _mediator = parameters.Mediator;
        _userRepository = parameters.UserRepository;
    }
    public abstract Task<TRespone> Handle(TRequest request, CancellationToken cancellationToken);
}

