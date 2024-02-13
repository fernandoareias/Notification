using Notification.Worker.Domain.Entities;
using Notification.Worker.Domain.Services.Interfaces;
using Notification.Worker.Infrastructure.ExternalServices.Letter.DTOs.Requests;
using Notification.Worker.Infrastructure.ExternalServices.Letter.Interfaces;

namespace Notification.Worker.Domain.Services;

public class LetterServices : ILetterServices
{
    public LetterServices(ILetterExternalService letterExternalService)
    {
        _letterExternalService = letterExternalService;
    }

    private readonly ILetterExternalService _letterExternalService;
    public async Task<Sent> Process(Notification aggregate)
    {
        var request = new SendLetterRequest();
        var response = await _letterExternalService.Send(request);
        return new Sent(response.ExternalId, response.PartnerSystem, response.Success);
    }
}