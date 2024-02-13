using FluentValidation;

namespace Notification.API.Application.Commands.Validations;

public class CreateNotificationValidations: AbstractValidator<CreateNotificationCommand>
{
    public CreateNotificationValidations()
    {
        RuleFor(c => c.Recipient)
            .NotNull()
            .NotEmpty();


        RuleFor(c => c.AggregateId)
            .NotNull();

        RuleFor(c => c.CreatedAt)
            .NotNull()
            .NotEmpty();

        RuleFor(c => c.MessageLayout)
            .NotNull()
            .NotEmpty();

        RuleFor(c => c.Recipient)
            .NotNull()
            .NotEmpty();

        RuleFor(c => c.Type)
            .NotNull()
            .NotEmpty();


        RuleForEach(c => c.Params).SetValidator(new CreateNotificationParamsCommandValidations());
    }
}



public class CreateNotificationParamsCommandValidations : AbstractValidator<CreateNotificationParamsCommand>
{
    public CreateNotificationParamsCommandValidations()
    {
        RuleFor(c => c.Key)
            .NotNull()
            .NotEmpty();

        RuleFor(c => c.Value)
            .NotNull()
            .NotEmpty();
    }
}