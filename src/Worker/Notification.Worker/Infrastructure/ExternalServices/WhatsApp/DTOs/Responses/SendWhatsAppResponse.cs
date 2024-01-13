namespace Notification.Worker.Domain.Services.DTOs.Responses;

public class SendWhatsAppResponse
{
    public SendWhatsAppResponse()
    {
        var number = new Random().Next(0, 100);
        PartnerSystem = $"Partner {number}"; 
        Success = number % 2 == 0;
    }
        
    public string ExternalId { get; private set; } = Guid.NewGuid().ToString();
    public string PartnerSystem { get; private set; }
    public bool Success { get; private set; } 
}