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
    readonly IHttpContextAccessor _httpContextAccessor;

    public IMediator Mediator => _mediator;
    public IUserRepository UserRepository => _userRepository;
    public TokenHelper TokenHelper => _tokenHelper;
    
    public UserInfo UserInfo { get; set; }
    public UserBaseRequestHandlerParameters(IMediator mediator, IUserRepository userRepository, TokenHelper tokenHelper, IHttpContextAccessor httpContextAccessor)
    {
        _mediator = mediator;
        _userRepository = userRepository;
        _tokenHelper = tokenHelper;
        _httpContextAccessor = httpContextAccessor;
        
        var userClaims = _httpContextAccessor.HttpContext?.User?.Claims;

        if (userClaims != null)
        {
            // Try to parse the NameIdentifier to an integer
            var userId = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            int parsedUserId = 0;
            if (!string.IsNullOrEmpty(userId) && !int.TryParse(userId, out parsedUserId))
            {
                // Handle the case where the ID could not be parsed
                throw new Exception("Invalid User ID in claims");
            }

            UserInfo = new UserInfo
            {
                ID = parsedUserId, // Set the parsed ID
                Name = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value,
                Email = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value
            };
        }
    }
    
}

