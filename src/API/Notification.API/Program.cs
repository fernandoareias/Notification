using System.Reflection;
using FluentValidation.AspNetCore;
using MediatR;
using Notification.API.Application.Commands;
using Notification.API.Middlewares;
using Notification.Core.Common.Validators;
using Notification.Core.Common.Validators.Interfaces;
using Notification.Core.Mediator;
using Notification.Core.Mediator.Behaviours;
using Notification.Core.Mediator.Interfaces;
using Notification.Core.MessageBus.Configurations;
using Notification.Core.MessageBus.Services;
using Notification.Core.MessageBus.Services.Interfaces;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFluentValidation(f => f.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(FailFastRequestBehavior<,>));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

builder.Services.Configure<MessageBusConfigs>(
    builder.Configuration.GetSection(nameof(MessageBusConfigs)));

builder.Services.AddScoped<IValidatorServices, ValidatorServices>();
builder.Services.AddScoped<IMediatorHandler, MediatorHandler>();
builder.Services.AddSingleton<IMessageBus, MessageBus>();

var app = builder.Build();

var counter = Metrics.CreateCounter("webapimetrics", "Count requests endpoints",
            new CounterConfiguration
            {
                LabelNames = new[] { "method", "endpoint" }
            });

app.Use((context, next) =>
{
    counter.WithLabels(context.Request.Method, context.Request.Path).Inc();
    return next();
});


app.UseMetricServer(settings => settings.EnableOpenMetrics = false);
app.UseHttpMetrics();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware(typeof(ExceptionMiddleware));

app.Run();