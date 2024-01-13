using Notification.Core.MessageBus.Services.Interfaces;
using Notification.Worker.Domain.Events;
using Notification.Worker.Workers.Base;

namespace Notification.Worker.Workers.Events;

public class NotificationPushDeliveryFailureEventWorker : NotificationDeliveryFailureEventWorker 
{
    public NotificationPushDeliveryFailureEventWorker(ILogger<NotificationPushDeliveryFailureEventWorker> logger, IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;

    }

    private readonly ILogger<NotificationPushDeliveryFailureEventWorker> _logger;
    private readonly IServiceProvider _serviceProvider;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("[WORKER[SEND-RETRY-Push] - Creating process...");
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("[WORKER[SEND-RETRY-Push] - Starting process...");
            using (var scope = _serviceProvider.CreateScope())
            {
                var messageBus = scope.ServiceProvider.GetRequiredService<IMessageBus>();

                messageBus.Subscribe<NotificationPushDeliveryFailureEvent>("notifications-failure", "push-delivery-failure-event", Process,
                    stoppingToken);
                await Task.Delay(-1, stoppingToken);
            }
        }
    }
     
}