using Microsoft.AspNetCore.Mvc;
using Notification.API.Application.Commands;
using Notification.API.DTOs.Requests;
using Notification.Core.Mediator.Interfaces;

namespace Notification.API.Controllers;

[Route("api/notiifcations")]
[ApiController]
public class NotificationController : ControllerBase
{
    public NotificationController(IMediatorHandler mediatorHandler)
    {
        _mediatorHandler = mediatorHandler;
    }

    private readonly IMediatorHandler _mediatorHandler;

    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateNotificationRequest request)
    {
        return await _mediatorHandler.Send<CreateNotificationCommand>(new CreateNotificationCommand(request));
    }
            
}