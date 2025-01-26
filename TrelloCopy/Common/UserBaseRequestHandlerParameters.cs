using FluentValidation;
using MediatR;
using TrelloCopy.Data.Repositories;
using TrelloCopy.Models;

namespace TrelloCopy.Common;
public class UserBaseRequestHandlerParameters
{
    readonly IMediator _mediator;
    readonly IUserRepository _userRepository;
    

    public IMediator Mediator => _mediator;
    public IUserRepository UserRepository => _userRepository;
    
    public UserBaseRequestHandlerParameters(IMediator mediator, IUserRepository userRepository)
    {
        _mediator = mediator;
        _userRepository = userRepository;
    }
    
}

