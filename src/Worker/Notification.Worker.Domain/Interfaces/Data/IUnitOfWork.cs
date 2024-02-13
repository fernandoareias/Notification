namespace Notification.Worker.Data.Interfaces;

public interface IUnitOfWork : IDisposable
{
    Task Commit();
}