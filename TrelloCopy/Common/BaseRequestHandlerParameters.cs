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
    readonly IHttpContextAccessor _httpContextAccessor;

    public IMediator Mediator => _mediator;
    public IRepository<BaseModel> Repository => _repository;
    public UserInfo UserInfo { get; set; }

    
    public BaseRequestHandlerParameters(IMediator mediator, IRepository<BaseModel> repository, IHttpContextAccessor httpContextAccessor)
    {
        _mediator = mediator;
        _repository = repository;
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

