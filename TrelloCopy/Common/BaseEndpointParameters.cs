using FluentValidation;
using MediatR;
using TrelloCopy.Common.Views;

namespace TrelloCopy.Common;
public class BaseEndpointParameters<TRequest>
{
    readonly IMediator _mediator;
    readonly IValidator<TRequest> _validator;

    public IMediator Mediator => _mediator;
    public IValidator<TRequest> Validator => _validator;
    public UserInfo UserInfo { get; set; }

    public BaseEndpointParameters(IMediator mediator, IValidator<TRequest> validator)
    {
        _mediator = mediator;
        _validator = validator;
    }
}

