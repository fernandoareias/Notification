using Notification.Core.Mediator.Interfaces;
using Notification.Worker.Application.Commands;
using Notification.Worker.Application.Commands.Factories;

namespace Notification.Worker.Workers.Base;

public abstract class BaseWorker : BackgroundService
{
    protected readonly IServiceProvider _serviceProvider;
    
    public BaseWorker(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
 
    }
    
    protected async Task Process(CreateNotificationCommand request)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var mediatorHandler = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
            var command = SendNotificationFactory.Create(request);
            await mediatorHandler.Send<CreateNotificationCommand>(command);
        }
    }
}