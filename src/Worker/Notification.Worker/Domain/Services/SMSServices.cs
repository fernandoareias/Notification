using Notification.Worker.Domain.Common;
using Notification.Worker.Domain.Entities;
using Notification.Worker.Domain.Enums;
using Notification.Worker.Domain.Services.Interfaces;
using Notification.Worker.Infrastructure.ExternalServices.SMS.DTOs.Requests;
using Notification.Worker.Infrastructure.ExternalServices.SMS.Interfaces;

namespace Notification.Worker.Domain.Services;

public class SMSServices : ISMSServices
{
    public SMSServices(ISMSExternalServices smsExternalServices, ILogger<SMSServices> logger)
    {
        _smsExternalServices = smsExternalServices;
        _logger = logger;
    }

    private readonly ISMSExternalServices _smsExternalServices;
    private readonly ILogger<SMSServices> _logger;

    
    public async Task<Sent> Process(Notification aggregate)
    {
        var request = new SendSMSRequest();
        var response = await _smsExternalServices.Send(request);
        return new Sent(response.ExternalId, response.PartnerSystem, response.Success);
    }
}