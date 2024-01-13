using Notification.Worker.Domain.Entities;
using Notification.Worker.Domain.Services.DTOs.Requests;
using Notification.Worker.Domain.Services.DTOs.Responses;
using Notification.Worker.Domain.Services.Interfaces;

namespace Notification.Worker.Domain.Services;

public class WhatsAppExternalService : IWhatsAppExternalService
{ 
    public async Task<SendWhatsAppResponse> Send(SendWhatsAppRequest request)
    {
        // TODO: REQUEST LOGIC TO PARTNER
        
        return new SendWhatsAppResponse();
    }
}