using Notification.Worker.Infrastructure.ExternalServices.Letter.DTOs.Requests;
using Notification.Worker.Infrastructure.ExternalServices.Letter.DTOs.Responses;
using Notification.Worker.Infrastructure.ExternalServices.Letter.Interfaces;

namespace Notification.Worker.Infrastructure.ExternalServices.Letter;

public class LetterExternalService : ILetterExternalService
{
    public Task<SendLetterResponse> Send(SendLetterRequest request)
    {
        throw new NotImplementedException();
    }
}