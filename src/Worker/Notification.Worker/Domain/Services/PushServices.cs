using Notification.Worker.Domain.Entities;
using Notification.Worker.Domain.Services.Interfaces;
using Notification.Worker.Infrastructure.ExternalServices.Push.DTOs.Requests;
using Notification.Worker.Infrastructure.ExternalServices.Push.Interfaces;

namespace Notification.Worker.Domain.Services;

public class PushServices : IPushServices
{
    public PushServices(IPushExternalService pushExternalService)
    {
        _pushExternalService = pushExternalService;
    }

    private readonly IPushExternalService _pushExternalService;
    public async Task<Sent> Process(Notification aggregate)
    {
        var request = new SendPushRequest();
        var response = await _pushExternalService.Send(request);
        return new Sent(response.ExternalId, response.PartnerSystem, response.Success);
    }
}