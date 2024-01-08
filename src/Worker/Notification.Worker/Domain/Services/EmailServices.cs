using Notification.Worker.Domain.Entities;
using Notification.Worker.Domain.Services.Interfaces;
using Notification.Worker.Infrastructure.ExternalServices.Email.Interfaces;

namespace Notification.Worker.Domain.Services;

public class EmailServices : IEmailServices
{
    public EmailServices(IEmailExternalService smsExternalServices, ILogger<EmailServices> logger)
    {
        _smsExternalServices = smsExternalServices;
        _logger = logger;
    }

    private readonly IEmailExternalService _smsExternalServices;
    private readonly ILogger<EmailServices> _logger;
    
    public Task<Sent> Process(Notification aggregate)
    {
        throw new NotImplementedException();
    }
}