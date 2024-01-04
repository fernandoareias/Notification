using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notification.Core.Common.CQRS;
using Notification.Core.Mediator.Interfaces;

namespace Notification.Core.Mediator;

public class MediatorHandler : IMediatorHandler
{
    private readonly IMediator _mediator;
    public MediatorHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> Send<TCommand>(Command command) where TCommand : Command
    {
        return await _mediator.Send(command);
    }

    public async Task<IActionResult> Execute<TQuery>(TQuery query) where TQuery : Query
    {
        return await _mediator.Send(query);
    }

}