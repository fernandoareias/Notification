using Notification.Worker.Infrastructure.ExternalServices.Base;
using Notification.Worker.Infrastructure.ExternalServices.Email.DTOs.Requests;
using Notification.Worker.Infrastructure.ExternalServices.Email.DTOs.Responses;

namespace Notification.Worker.Infrastructure.ExternalServices.Email.Interfaces;

public interface IEmailExternalService : IBaseExternalServices<SendEmailResponse, SendEmailRequest>
{
    
}