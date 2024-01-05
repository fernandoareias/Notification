using Notification.Core.Mediator.Interfaces;
using Notification.Core.MessageBus.Services.Interfaces;
using Notification.Worker.Application.Commands;
using Notification.Worker.Application.Commands.Factories;

namespace Notification.Worker.Workers;

public class SendSMSWorker: BackgroundService
{
    public SendSMSWorker(ILogger<SendSMSWorker> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
         
    }

    private readonly ILogger<SendSMSWorker> _logger;
    private readonly IServiceProvider _serviceProvider;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            Console.WriteLine("[WORKER[SEND-SMS] - Creating process...");
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

    private void Process(CreateNotificationCommand request)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var mediatorHandler = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
            var command = SendNotificationFactory.Create(request); 
            mediatorHandler.Send<CreateNotificationCommand>(command).GetAwaiter();
        }
    }
}