using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Notification.Core.Common.CQRS;
using Notification.Core.Common.Enums;
using Notification.Core.Common.Validators.Interfaces;

namespace Notification.Core.Mediator.Behaviours;

public class FailFastRequestBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
     where TRequest : IRequest<TResponse> where TResponse : View
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    private readonly IValidatorServices _validatorServices;
    public FailFastRequestBehavior(IEnumerable<IValidator<TRequest>> validators, IValidatorServices validatorServices)
    {
        _validators = validators;
        _validatorServices = validatorServices;
    }


    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);
        var failures = _validators
            .Select(v => v.Validate(context))
            .SelectMany(result => result.Errors)
            .Where(f => f != null)
            .ToList();

        if (!failures.Any())
            return next();

        failures.ForEach(c => _validatorServices.AddError(EBaseErro.INVALID_FIELD.ToString(), c.ErrorMessage));

        return Task.FromResult<TResponse>(null);
    }

}