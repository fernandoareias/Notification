using Notification.Worker.Domain.Entities;
using Notification.Worker.Domain.Services.DTOs.Requests;
using Notification.Worker.Domain.Services.Interfaces;

namespace Notification.Worker.Domain.Services;

public class WhatsAppServices : IWhatsAppServices
{
    public WhatsAppServices(IWhatsAppExternalService whatsAppExternalService)
    {
        _whatsAppExternalService = whatsAppExternalService;
    }

    private readonly IWhatsAppExternalService _whatsAppExternalService;
    public async Task<Sent> Process(Notification aggregate)
    {
        var request = new SendWhatsAppRequest();
        var response = await _whatsAppExternalService.Send(request);
        return new Sent(response.ExternalId, response.PartnerSystem, response.Success);
    }
}