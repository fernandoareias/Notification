using Notification.Worker.Infrastructure.ExternalServices.Push.DTOs.Requests;
using Notification.Worker.Infrastructure.ExternalServices.Push.DTOs.Responses;
using Notification.Worker.Infrastructure.ExternalServices.Push.Interfaces;

namespace Notification.Worker.Infrastructure.ExternalServices.Push;

public class PushExternalService : IPushExternalService
{
    public async Task<SendPushResponse> Send(SendPushRequest request)
    {
        // TODO: REQUEST LOGIC TO PARTNER
        
        return new SendPushResponse();
    }
}