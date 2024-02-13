using MongoDB.Driver;
using Notification.Worker.Data.Interfaces;
using Notification.Worker.Data.Repositories.Interfaces;
using Notification.Core.Common.CQRS;

namespace Notification.Worker.Data.Repositories;

public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : AggregateRoot
{
    protected readonly IMongoContext Context;
    protected IMongoCollection<TEntity> DbSet;

    public IUnitOfWork unitOfWork => Context;

    protected BaseRepository(IMongoContext context)
    {
        Context = context;

        DbSet = Context.GetCollection<TEntity>(typeof(TEntity).Name); 
    }

    public virtual void Add(TEntity obj)
    {
        Context.AddCommand(() => DbSet.InsertOneAsync(obj), obj);
    }

    public virtual async Task<TEntity> GetById(Guid id)
    {
        var data = await DbSet.FindAsync(Builders<TEntity>.Filter.Eq("_id", id));
        return data.SingleOrDefault();
    }

    public virtual async Task<IEnumerable<TEntity>> GetAll()
    {
        var all = await DbSet.FindAsync(Builders<TEntity>.Filter.Empty);
        return all.ToList();
    }

    public virtual void Update(TEntity obj)
    {
        Context.AddCommand(() => DbSet.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", obj._id), obj), obj);
    }

    public virtual void Remove(Guid id)
    {
        Context.AddCommand(() => DbSet.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", id)));
    }

    public void Dispose()
    {
        Context?.Dispose();
    }
}