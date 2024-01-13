using Notification.Worker.Infrastructure.ExternalServices.Base;
using Notification.Worker.Infrastructure.ExternalServices.SMS.DTOs.Requests;
using Notification.Worker.Infrastructure.ExternalServices.SMS.DTOs.Responses;

namespace Notification.Worker.Infrastructure.ExternalServices.SMS.Interfaces;

public interface ISMSExternalServices : IBaseExternalServices<SendSMSResponse,SendSMSRequest>
{
}