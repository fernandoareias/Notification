using Notification.Worker.Data.Interfaces;

namespace Notification.Worker.Data.Repositories.Interfaces;

public interface IRepository<TEntity> : IDisposable where TEntity : class
{
    void Add(TEntity obj);
    Task<TEntity> GetById(Guid id);
    Task<IEnumerable<TEntity>> GetAll();
    void Update(TEntity obj);
    void Remove(Guid id);

    IUnitOfWork unitOfWork { get; }
}