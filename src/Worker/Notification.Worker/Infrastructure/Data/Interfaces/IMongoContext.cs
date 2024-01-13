using MongoDB.Driver;
using Notification.Core.Common.CQRS;

namespace Notification.Worker.Data.Interfaces;

public interface IMongoContext : IUnitOfWork
{
    void AddCommand(Func<Task> func,  AggregateRoot? entity = null);
    Task<int> SaveChanges();
    IMongoCollection<T> GetCollection<T>(string name);
}