using Notification.Worker.Infrastructure.ExternalServices.SMS.DTOs.Requests;
using Notification.Worker.Infrastructure.ExternalServices.SMS.DTOs.Responses;
using Notification.Worker.Infrastructure.ExternalServices.SMS.Interfaces;

namespace Notification.Worker.Infrastructure.ExternalServices.SMS;

public class SMSExternalServices : ISMSExternalServices
{
    public async Task<SendSMSResponse> Send(SendSMSRequest request)
    {
        // TODO: REQUEST LOGIC TO PARTNER

        return new SendSMSResponse();
    }
}