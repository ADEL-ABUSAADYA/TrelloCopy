using FluentValidation;
using MediatR;
using TrelloCopy.Common.Views;
using TrelloCopy.Data.Repositories;
using TrelloCopy.Helpers;
using TrelloCopy.Models;

namespace TrelloCopy.Common
{
    public class BaseRequestHandlerParameters<TEntity> where TEntity : BaseModel
    {
        private readonly IMediator _mediator;
        private readonly IRepository<TEntity> _repository;
        private readonly TokenHelper _tokenHelper;
        private readonly UserInfo _userInfo;

        public IMediator Mediator => _mediator;
        public IRepository<TEntity> Repository => _repository;
        public TokenHelper TokenHelper => _tokenHelper;
        public UserInfo UserInfo => _userInfo;
        

        // Constructor accepts the generic repository type for flexibility
        public BaseRequestHandlerParameters(IMediator mediator, IRepository<TEntity> repository, UserInfoProvider userInfoProvider, TokenHelper tokenHelper)
        {
            _mediator = mediator;
            _repository = repository;
            _userInfo = userInfoProvider.UserInfo;
            _tokenHelper = tokenHelper;
            
        }
    }
}
