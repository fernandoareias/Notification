using Microsoft.AspNetCore.Mvc;
using Notification.API.Application.Commands;
using Notification.API.Application.Commands.Views;
using Notification.API.DTOs.Requests;
using Notification.Core.Common.CQRS;
using Notification.Core.Common.Validators.Interfaces;
using Notification.Core.Mediator.Interfaces;

namespace Notification.API.Controllers;

[Route("api/notiifcations")]
[ApiController]
public class NotificationController : BaseController
{
    public NotificationController(IMediatorHandler mediatorHandler, IValidatorServices validatorServices) : base(mediatorHandler, validatorServices)
    {
        
    }

    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateNotificationRequest request)
    {
        var response = await _mediatorHandler.Send<CreateNotificationCommand>(new CreateNotificationCommand(request));

        if (response is null)
            return ReturnBadRequestComErros<CreateNotificationCommandView>();

        var view = response as CreateNotificationCommandView;

        return Created(view!.CorrelationId.ToString(), response);
    }
            
}