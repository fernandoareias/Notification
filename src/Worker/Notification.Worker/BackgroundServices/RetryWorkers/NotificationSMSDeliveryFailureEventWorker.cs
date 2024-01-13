using Notification.Core.MessageBus.Services.Interfaces;
using Notification.Worker.Domain.Events;
using Notification.Worker.Workers.Base;

namespace Notification.Worker.Workers.Events;


public class NotificationSMSDeliveryFailureEventWorker : NotificationDeliveryFailureEventWorker 
{
    public NotificationSMSDeliveryFailureEventWorker(ILogger<NotificationSMSDeliveryFailureEventWorker> logger, IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;

    }

    private readonly ILogger<NotificationSMSDeliveryFailureEventWorker> _logger;
    private readonly IServiceProvider _serviceProvider;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("[WORKER[EVENT-RETRY-SMS] - Creating process...");
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("[WORKER[EVENT-RETRY-SMS] - Starting process...");
            using (var scope = _serviceProvider.CreateScope())
            {
                var messageBus = scope.ServiceProvider.GetRequiredService<IMessageBus>();

                messageBus.Subscribe<NotificationPushDeliveryFailureEvent>("notifications-failure", "sms-delivery-failure-event", Process,
                    stoppingToken);
                await Task.Delay(-1, stoppingToken);
            }
        }
    }
     
}