using System.Reflection;
using MediatR;
using Notification.Core.Mediator;
using Notification.Core.Mediator.Behaviours;
using Notification.Core.Mediator.Interfaces;
using Notification.Core.MessageBus.Configurations;
using Notification.Core.MessageBus.Services;
using Notification.Core.MessageBus.Services.Interfaces;
using Notification.Worker;
using Notification.Worker.Workers;
using ServiceCollectionExtensions = Microsoft.Extensions.DependencyInjection.ServiceCollectionExtensions;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
         
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        ServiceCollectionExtensions.AddMediatR(services, cfg=>cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        services.Configure<MessageBusConfigs>(
            hostContext.Configuration.GetSection(nameof(MessageBusConfigs)));

        services.AddScoped<IMediatorHandler, MediatorHandler>();
        services.AddSingleton<IMessageBus, MessageBus>();

        services.AddHostedService<SendSMSWorker>();
        //services.AddHostedService<SendEmailWorker>();
    })
    .Build();

await host.RunAsync();