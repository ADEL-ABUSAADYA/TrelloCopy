using System.Security.Claims;
using FluentValidation;
using MediatR;
using TrelloCopy.Common.Views;
using TrelloCopy.Data.Repositories;
using TrelloCopy.Helpers;
using TrelloCopy.Models;

namespace TrelloCopy.Common;
public class UserBaseRequestHandlerParameters
{
    readonly IMediator _mediator;
    readonly IUserRepository _userRepository;
    readonly TokenHelper _tokenHelper;

    public IMediator Mediator => _mediator;
    public IUserRepository UserRepository => _userRepository;
    public TokenHelper TokenHelper => _tokenHelper;
    public UserInfo UserInfo { get; set; }
    
    
    public UserBaseRequestHandlerParameters(IMediator mediator, IUserRepository userRepository, TokenHelper tokenHelper, UserInfo userInfo)
    {
        _mediator = mediator;
        _userRepository = userRepository;
        _tokenHelper = tokenHelper;
        UserInfo = userInfo;
    }
    
}

