using MediatR;
using TrelloCopy.Common.Views;
using TrelloCopy.Data.Repositories;
using TrelloCopy.Helpers;
using TrelloCopy.Models;

namespace TrelloCopy.Common.BaseHandlers
{
    public abstract class BaseRequestHandler<TRequest, TResponse, TEntity> : IRequestHandler<TRequest, TResponse> 
        where TRequest : IRequest<TResponse>
        where TEntity : BaseModel
    {
        protected readonly IMediator _mediator;
        protected readonly IRepository<TEntity> _repository;
        protected readonly TokenHelper _tokenHelper;
        protected readonly UserInfo _userInfo;
        
        public BaseRequestHandler(BaseRequestHandlerParameters<TEntity> parameters)
        {
            _mediator = parameters.Mediator;
            _repository = parameters.Repository;
            _userInfo = parameters.UserInfo;
            _tokenHelper = parameters.TokenHelper;

            
        }
        
        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}