using Notification.Worker.Domain.Entities;
using Notification.Worker.Domain.Services.Interfaces;
using Notification.Worker.Infrastructure.ExternalServices.Email.DTOs.Requests;
using Notification.Worker.Infrastructure.ExternalServices.Email.Interfaces;

namespace Notification.Worker.Domain.Services;

public class EmailServices : IEmailServices
{
    public EmailServices(IEmailExternalService emailExternalService)
    {
        _emailExternalService = emailExternalService;
    }

    private readonly IEmailExternalService _emailExternalService;
    
    public async Task<Sent> Process(Notification aggregate)
    {
        var request = new SendEmailRequest();
        var response = await _emailExternalService.Send(request);
        return new Sent(response.ExternalId, response.PartnerSystem, response.Success);
    }
}