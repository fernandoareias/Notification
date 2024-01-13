using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notification.Core.Common.CQRS;

namespace Notification.Core.Mediator.Interfaces;

public interface IMediatorHandler
{
    Task Publish<TEvent>(TEvent @event) where TEvent : Event;
    Task<IActionResult> Send<TCommand>(Command command) where TCommand : Command;
    Task<IActionResult> Execute<TQuery>(TQuery query) where TQuery : Query;
}