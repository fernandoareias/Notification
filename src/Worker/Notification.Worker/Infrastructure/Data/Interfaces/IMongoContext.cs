using MongoDB.Driver;

namespace Notification.Worker.Data.Interfaces;

public interface IMongoContext : IUnitOfWork
{
    void AddCommand(Func<Task> func);
    Task<int> SaveChanges();
    IMongoCollection<T> GetCollection<T>(string name);
}