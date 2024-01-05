using Notification.Core.Mediator.Interfaces;
using Notification.Core.MessageBus.Services.Interfaces;
using Notification.Worker.Application.Commands;
using Notification.Worker.Application.Commands.Factories;

namespace Notification.Worker.Workers;

public class SendEmailWorker : BackgroundService
{
    public SendEmailWorker(ILogger<SendSMSWorker> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
 
    }

    private readonly ILogger<SendSMSWorker> _logger;
    private readonly IServiceProvider _serviceProvider;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("[WORKER[SEND-EMAIL] - Creating process...");
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("[WORKER[SEND-EMAIL] - Starting process...");
            using (var scope = _serviceProvider.CreateScope())
            {
                var messageBus = scope.ServiceProvider.GetRequiredService<IMessageBus>();
                var mediatorHandler = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();

                messageBus.Subscribe<CreateNotificationCommand>("notifications", "send-notification-EMAIL", Process,
                    stoppingToken);

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