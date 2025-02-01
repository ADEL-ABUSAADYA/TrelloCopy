using MediatR;
using TrelloCopy.Common.Views;
using TrelloCopy.Data.Repositories;
using TrelloCopy.Helpers;
using TrelloCopy.Models;

namespace TrelloCopy.Common.BaseHandlers
{
    public abstract class BaseWithoutRepositoryRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> 
        where TRequest : IRequest<TResponse>
    {
        protected readonly IMediator _mediator;
        protected readonly TokenHelper _tokenHelper;
        protected readonly UserInfo _userInfo;
        
        public BaseWithoutRepositoryRequestHandler(BaseWithotRepositoryRequestHandlerParameters parameters)
        {
            _mediator = parameters.Mediator;
            _userInfo = parameters.UserInfo;
            _tokenHelper = parameters.TokenHelper;

            
        }
        
        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}