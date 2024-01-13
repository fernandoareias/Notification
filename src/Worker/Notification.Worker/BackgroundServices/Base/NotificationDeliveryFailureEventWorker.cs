using Notification.Core.Mediator.Interfaces;
using Notification.Worker.Application.Commands;
using Notification.Worker.Application.Commands.Factories;
using Notification.Worker.Domain.Events.Common;

namespace Notification.Worker.Workers.Base;

public abstract class NotificationDeliveryFailureEventWorker : BackgroundService
{
    protected readonly IServiceProvider _serviceProvider;
    
    public NotificationDeliveryFailureEventWorker(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
 
    }
    
    protected async Task Process(NotificationDeliveryFailureEvent request)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var mediatorHandler = scope.ServiceProvider.GetRequiredService<IMediatorHandler>(); 
            await mediatorHandler.Publish(request);
        }
    }
}