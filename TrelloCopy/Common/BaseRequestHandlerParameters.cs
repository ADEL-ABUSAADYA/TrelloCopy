using FluentValidation;
using MediatR;
using TrelloCopy.Data.Repositories;
using TrelloCopy.Models;

namespace TrelloCopy.Common;
public class BaseRequestHandlerParameters
{
    readonly IMediator _mediator;
    readonly IRepository<BaseModel> _repository;
    readonly IUserRepository _userRepository;

    public IMediator Mediator => _mediator;
    public IRepository<BaseModel> Repository => _repository;
    public IUserRepository UserRepository => _userRepository;
    
    public BaseRequestHandlerParameters(IMediator mediator, IRepository<BaseModel> repository, IUserRepository userRepository)
    {
        _mediator = mediator;
        _repository = repository;
        _userRepository = userRepository;
    }
    
}

