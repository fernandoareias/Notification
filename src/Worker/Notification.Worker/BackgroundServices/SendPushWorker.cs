using Notification.Core.MessageBus.Services.Interfaces;
using Notification.Worker.Application.Commands;
using Notification.Worker.Workers.Base;

namespace Notification.Worker.Workers;
 
public class SendPushWorker : BaseWorker
{
    public SendPushWorker(ILogger<SendPushWorker> logger, IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _logger = logger;
    }

    private readonly ILogger<SendPushWorker> _logger;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("[WORKER[SEND-Push] - Creating process...");

        while (!stoppingToken.IsCancellationRequested)
        {
            Console.WriteLine("[WORKER[SEND-Push] - Starting process...");
            using (var scope = _serviceProvider.CreateScope())
            {
                var messageBus = scope.ServiceProvider.GetRequiredService<IMessageBus>();
                messageBus.Subscribe<CreateNotificationCommand>("notifications", "send-notification-PushNotification", Process,
                    stoppingToken);

                Console.WriteLine("[WORKER[SEND-Push] - Awaiting process...");
                await Task.Delay(-1, stoppingToken);
            }
        }
       
    }
 
}