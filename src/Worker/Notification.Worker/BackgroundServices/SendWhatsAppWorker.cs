using Notification.Core.MessageBus.Services.Interfaces;
using Notification.Worker.Application.Commands;
using Notification.Worker.Workers.Base;

namespace Notification.Worker.Workers;
 
public class SendWhatsAppWorker : BaseWorker
{
    public SendWhatsAppWorker(ILogger<SendWhatsAppWorker> logger, IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _logger = logger;
         
    }

    private readonly ILogger<SendWhatsAppWorker> _logger;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("[WORKER[SEND-WhatsApp] - Creating process...");

        while (!stoppingToken.IsCancellationRequested)
        {
            Console.WriteLine("[WORKER[SEND-WhatsApp] - Starting process...");
            using (var scope = _serviceProvider.CreateScope())
            {
                var messageBus = scope.ServiceProvider.GetRequiredService<IMessageBus>();
                messageBus.Subscribe<CreateNotificationCommand>("notifications", "send-notification-WhatsApp", Process,
                    stoppingToken);

                Console.WriteLine("[WORKER[SEND-WhatsApp] - Awaiting process...");
                await Task.Delay(-1, stoppingToken);
            }
        }
       
    }

}