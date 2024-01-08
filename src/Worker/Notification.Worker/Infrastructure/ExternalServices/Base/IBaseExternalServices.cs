namespace Notification.Worker.Infrastructure.ExternalServices.Base;

public interface IBaseExternalServices<TResult, TRequest>
{
    Task<TResult> Send(TRequest request);
}