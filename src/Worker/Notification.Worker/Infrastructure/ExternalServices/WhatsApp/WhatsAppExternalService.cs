using Notification.Worker.Domain.Entities;
using Notification.Worker.Domain.Services.DTOs.Requests;
using Notification.Worker.Domain.Services.DTOs.Responses;
using Notification.Worker.Domain.Services.Interfaces;

namespace Notification.Worker.Domain.Services;

public class WhatsAppExternalService : IWhatsAppExternalService
{ 
    public Task<SendWhatsAppResponse> Send(SendWhatsAppRequest request)
    {
        throw new NotImplementedException();
    }
}