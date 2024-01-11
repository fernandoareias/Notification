using Notification.Core.Mediator.Interfaces;
using Notification.Core.MessageBus.Services.Interfaces;
using Notification.Worker.Application.Commands;
using Notification.Worker.Application.Commands.Factories;
using Notification.Worker.Workers.Base;

namespace Notification.Worker.Workers;

public class SendLetterWorker : CreateNotificationCommandWorker
{
    public SendLetterWorker(ILogger<SendLetterWorker> logger, IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _logger = logger;
    }

    private readonly ILogger<SendLetterWorker> _logger;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("[WORKER[SEND-Letter] - Creating process...");

        while (!stoppingToken.IsCancellationRequested)
        {
            Console.WriteLine("[WORKER[SEND-Letter] - Starting process...");
            using (var scope = _serviceProvider.CreateScope())
            {
                var messageBus = scope.ServiceProvider.GetRequiredService<IMessageBus>();
                messageBus.Subscribe<CreateNotificationCommand>("notifications", "send-notification-Letter", Process,
                    stoppingToken);

                Console.WriteLine("[WORKER[SEND-Letter] - Awaiting process...");
                await Task.Delay(-1, stoppingToken);
            }
        }
       
    }
 
}

