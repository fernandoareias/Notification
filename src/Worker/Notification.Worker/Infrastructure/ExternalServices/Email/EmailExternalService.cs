using Notification.Worker.Infrastructure.ExternalServices.Email.DTOs.Requests;
using Notification.Worker.Infrastructure.ExternalServices.Email.DTOs.Responses;
using Notification.Worker.Infrastructure.ExternalServices.Email.Interfaces;

namespace Notification.Worker.Infrastructure.ExternalServices.Email;

public class EmailExternalService : IEmailExternalService
{
    public Task<SendEmailResponse> Send(SendEmailRequest request)
    {
        throw new NotImplementedException();
    }
}