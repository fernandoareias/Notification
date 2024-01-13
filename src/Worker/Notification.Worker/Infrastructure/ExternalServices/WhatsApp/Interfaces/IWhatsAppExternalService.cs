using Notification.Worker.Domain.Services.DTOs.Requests;
using Notification.Worker.Domain.Services.DTOs.Responses;
using Notification.Worker.Infrastructure.ExternalServices.Base;

namespace Notification.Worker.Domain.Services.Interfaces;

public interface IWhatsAppExternalService : IBaseExternalServices<SendWhatsAppResponse, SendWhatsAppRequest>
{
    
}