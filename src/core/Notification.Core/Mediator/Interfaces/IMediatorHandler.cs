using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notification.Core.Common.CQRS;

namespace Notification.Core.Mediator.Interfaces;

public interface IMediatorHandler
{
    Task Publish<TEvent>(TEvent @event) where TEvent : Event;
    Task<View> Send<TCommand>(Command command) where TCommand : Command;
    Task<List<View>> Execute<TQuery>(TQuery query) where TQuery : Query;
}