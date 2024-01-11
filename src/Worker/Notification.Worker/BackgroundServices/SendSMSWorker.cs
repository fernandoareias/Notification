using Notification.Core.Mediator.Interfaces;
using Notification.Core.MessageBus.Services.Interfaces;
using Notification.Worker.Application.Commands;
using Notification.Worker.Application.Commands.Factories;
using Notification.Worker.Workers.Base;

namespace Notification.Worker.Workers;

public class SendSMSWorker : CreateNotificationCommandWorker
{
    public SendSMSWorker(ILogger<SendSMSWorker> logger, IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _logger = logger;
         
    }

    private readonly ILogger<SendSMSWorker> _logger;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("[WORKER[SEND-SMS] - Creating process...");

        while (!stoppingToken.IsCancellationRequested)
        {
            Console.WriteLine("[WORKER[SEND-SMS] - Starting process...");
            using (var scope = _serviceProvider.CreateScope())
            {
                var messageBus = scope.ServiceProvider.GetRequiredService<IMessageBus>();
                messageBus.Subscribe<CreateNotificationCommand>("notifications", "send-notification-SMS", Process,
                    stoppingToken);

                Console.WriteLine("[WORKER[SEND-SMS] - Awaiting process...");
                await Task.Delay(-1, stoppingToken);
            }
        }
       
    }

}