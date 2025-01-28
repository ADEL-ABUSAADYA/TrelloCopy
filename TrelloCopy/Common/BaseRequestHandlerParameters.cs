using System.Security.Claims;
using FluentValidation;
using MediatR;
using TrelloCopy.Common.Views;
using TrelloCopy.Data.Repositories;
using TrelloCopy.Models;

namespace TrelloCopy.Common;
public class BaseRequestHandlerParameters
{
    readonly IMediator _mediator;
    readonly IRepository<BaseModel> _repository;

    public IMediator Mediator => _mediator;
    public IRepository<BaseModel> Repository => _repository;
    public UserInfo UserInfo { get; set; }

    
    public BaseRequestHandlerParameters(IMediator mediator, IRepository<BaseModel> repository, UserInfo userInfo)
    {
        _mediator = mediator;
        _repository = repository;
        UserInfo = userInfo;
    }
    
}

