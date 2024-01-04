using FluentValidation;

namespace Notification.API.Application.Commands.Validations;

public class CreateNotificationValidations: AbstractValidator<CreateNotificationCommand>
{
    public CreateNotificationValidations()
    {
        
    }
}