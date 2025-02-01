using FluentValidation;
using MediatR;
using TrelloCopy.Common.Views;
using TrelloCopy.Data.Repositories;
using TrelloCopy.Helpers;
using TrelloCopy.Models;

namespace TrelloCopy.Common.BaseHandlers
{
    public class BaseWithotRepositoryRequestHandlerParameters
    {
        private readonly IMediator _mediator;
        private readonly TokenHelper _tokenHelper;
        private readonly UserInfo _userInfo;

        public IMediator Mediator => _mediator;
        public TokenHelper TokenHelper => _tokenHelper;
        public UserInfo UserInfo => _userInfo;
        

        // Constructor accepts the generic repository type for flexibility
        public BaseWithotRepositoryRequestHandlerParameters(IMediator mediator, UserInfoProvider userInfoProvider, TokenHelper tokenHelper)
        {
            _mediator = mediator;
            _userInfo = userInfoProvider.UserInfo;
            _tokenHelper = tokenHelper;
            
        }
    }
}
