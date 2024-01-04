using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Notification.Core.Mediator.Behaviours;
//
// public class ValidationRequestBehavior<TRequest, IActionResult> : IPipelineBehavior<TRequest, IActionResult>
// {
//     private readonly IEnumerable<IValidator> _validators;
//
//     public ValidationRequestBehavior(IEnumerable<IValidator<TRequest>> validators)
//     {
//         _validators = validators;
//     }
//  
//     public Task<IActionResult> Handle(TRequest request, RequestHandlerDelegate<IActionResult> next, CancellationToken cancellationToken)
//     {
//         var failures = _validators
//             .Select(v => v.Validate(request))
//             .SelectMany(x => x.Errors)
//             .Where(f => f != null)
//             .ToList();
//
//         return failures.Any() ? null : next();
//     }
// }