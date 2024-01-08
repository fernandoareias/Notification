using System.Reflection;
using MediatR;
using Notification.Core.Mediator;
using Notification.Core.Mediator.Behaviours;
using Notification.Core.Mediator.Interfaces;
using Notification.Core.MessageBus.Configurations;
using Notification.Core.MessageBus.Services;
using Notification.Core.MessageBus.Services.Interfaces;
using Notification.Worker;
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
using ServiceCollectionExtensions = Microsoft.Extensions.DependencyInjection.ServiceCollectionExtensions;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
         
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        ServiceCollectionExtensions.AddMediatR(services, cfg=>cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        services.Configure<MessageBusConfigs>(
            hostContext.Configuration.GetSection(nameof(MessageBusConfigs)));
 
        services.AddScoped<IEmailServices, EmailServices>();
        services.AddScoped<ILetterServices, LetterServices>();
        services.AddScoped<IPushServices, PushServices>();
        services.AddScoped<ISMSServices, SMSServices>();
        services.AddScoped<IWhatsAppServices, WhatsAppServices>();
        
        services.AddScoped<IMediatorHandler, MediatorHandler>();
        services.AddScoped<IEmailExternalService, EmailExternalService>();
        services.AddScoped<ILetterExternalService, LetterExternalService>();
        services.AddScoped<IPushExternalService, PushExternalService>();
        services.AddScoped<ISMSExternalServices, SMSExternalServices>();
        
        services.AddSingleton<IMessageBus, MessageBus>(); 

        services.AddHostedService<SendSMSWorker>();
        services.AddHostedService<SendEmailWorker>();
        services.AddHostedService<SendLetterWorker>();
        services.AddHostedService<SendPushWorker>();
        services.AddHostedService<SendWhatsAppWorker>();
    })
    .Build();

await host.RunAsync();