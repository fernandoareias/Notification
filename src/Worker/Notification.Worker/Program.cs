using System.Reflection;
using MediatR;
using Notification.Core.Mediator;
using Notification.Core.Mediator.Behaviours;
using Notification.Core.Mediator.Interfaces;
using Notification.Core.MessageBus.Configurations;
using Notification.Core.MessageBus.Services;
using Notification.Core.MessageBus.Services.Interfaces;
using Notification.Worker;
using Notification.Worker.Data;
using Notification.Worker.Data.Interfaces;
using Notification.Worker.Data.Repositories;
using Notification.Worker.Domain.Services;
using Notification.Worker.Domain.Services.Interfaces;
using Notification.Worker.Infrastructure.ExternalServices.Email;
using Notification.Worker.Infrastructure.ExternalServices.Email.Interfaces;
using Notification.Worker.Infrastructure.ExternalServices.Letter;
using Notification.Worker.Infrastructure.ExternalServices.Letter.Interfaces;
using Notification.Worker.Infrastructure.ExternalServices.Push;
using Notification.Worker.Infrastructure.ExternalServices.Push.Interfaces;
using Notification.Worker.Infrastructure.ExternalServices.SMS;
using Notification.Worker.Infrastructure.ExternalServices.SMS.Interfaces;
using Notification.Worker.Workers;
using Notification.Worker.Workers.Events;
using ServiceCollectionExtensions = Microsoft.Extensions.DependencyInjection.ServiceCollectionExtensions;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
         
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        ServiceCollectionExtensions.AddMediatR(services, cfg=>cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        services.Configure<MessageBusConfigs>(
            hostContext.Configuration.GetSection(nameof(MessageBusConfigs)));
 
        services.AddScoped<IMessageBus, MessageBus>();
        services.AddScoped<IMongoContext, MongoContext>();
        services.AddScoped<IUnitOfWork, MongoContext>();
        services.AddScoped<Notification.Worker.Data.Repositories.Interfaces.INotificationRepository, NotificationRepository>();
        
        services.AddScoped<IMediatorHandler, MediatorHandler>();
        services.AddScoped<IEmailExternalService, EmailExternalService>();
        services.AddScoped<ILetterExternalService, LetterExternalService>();
        services.AddScoped<IPushExternalService, PushExternalService>();
        services.AddScoped<ISMSExternalServices, SMSExternalServices>();
        services.AddScoped<IWhatsAppExternalService, WhatsAppExternalService>();

        services.AddScoped<IEmailServices, EmailServices>();
        services.AddScoped<ILetterServices, LetterServices>();
        services.AddScoped<IPushServices, PushServices>();
        services.AddScoped<ISMSServices, SMSServices>();
        services.AddScoped<IWhatsAppServices, WhatsAppServices>();

        
        services.AddHostedService<SendSMSWorker>();
        services.AddHostedService<SendEmailWorker>();
        services.AddHostedService<SendLetterWorker>();
        services.AddHostedService<SendPushWorker>();
        services.AddHostedService<SendWhatsAppWorker>();
        
        services.AddHostedService<NotificationSMSDeliveryFailureEventWorker>();
        services.AddHostedService<NotificationEmailDeliveryFailureEventWorker>();
        services.AddHostedService<NotificationLetterDeliveryFailureEventWorker>();
        services.AddHostedService<NotificationPushDeliveryFailureEventWorker>();
        services.AddHostedService<NotificationWhatsAppDeliveryFailureEventWorker>();
    })
    .Build();

await host.RunAsync();