using Notification.Core.Mediator.Interfaces;
using Notification.Core.MessageBus.Services.Interfaces;
using Notification.Worker.Application.Commands;
using Notification.Worker.Domain.Events;
using Notification.Worker.Workers.Base;

namespace Notification.Worker.Workers.Events;
 
public class NotificationEmailDeliveryFailureEventWorker : NotificationDeliveryFailureEventWorker 
{
    public NotificationEmailDeliveryFailureEventWorker(ILogger<NotificationEmailDeliveryFailureEventWorker> logger, IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;

    }

    private readonly ILogger<NotificationEmailDeliveryFailureEventWorker> _logger;
    private readonly IServiceProvider _serviceProvider;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("[WORKER[SEND-RETRY-EMAIL] - Creating process...");
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("[WORKER[SEND-RETRY-EMAIL] - Starting process...");
            using (var scope = _serviceProvider.CreateScope())
            {
                var messageBus = scope.ServiceProvider.GetRequiredService<IMessageBus>();

                messageBus.Subscribe<NotificationEmailDeliveryFailureEvent>("notifications-failure", "email-delivery-failure-event", Process,
                    stoppingToken);
                await Task.Delay(-1, stoppingToken);
            }
        }
    }
     
}