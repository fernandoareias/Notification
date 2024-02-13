using Notification.Worker.Infrastructure.ExternalServices.Base;
using Notification.Worker.Infrastructure.ExternalServices.Letter.DTOs.Requests;
using Notification.Worker.Infrastructure.ExternalServices.Letter.DTOs.Responses;

namespace Notification.Worker.Infrastructure.ExternalServices.Letter.Interfaces;

public interface ILetterExternalService : IBaseExternalServices<SendLetterResponse, SendLetterRequest>
{
    
}