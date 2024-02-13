using Notification.Worker.Infrastructure.ExternalServices.Base;
using Notification.Worker.Infrastructure.ExternalServices.Push.DTOs.Requests;
using Notification.Worker.Infrastructure.ExternalServices.Push.DTOs.Responses;

namespace Notification.Worker.Infrastructure.ExternalServices.Push.Interfaces;

public interface IPushExternalService : IBaseExternalServices<SendPushResponse,SendPushRequest>
{
    
}