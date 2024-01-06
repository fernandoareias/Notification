using MongoDB.Driver;
using Notification.Worker.Data.Interfaces;

namespace Notification.Worker.Data;

public class MongoContext : IMongoContext
{
    private IMongoDatabase Database { get; set; }
    public IClientSessionHandle Session { get; set; }
    public MongoClient MongoClient { get; set; }
    private readonly List<Func<Task>> _commands;
    private readonly IConfiguration _configuration;
         

    public MongoContext(IConfiguration configuration)
    {
        _configuration = configuration;

        _commands = new List<Func<Task>>();
    }

    public async Task<int> SaveChanges()
    {
        ConfigureMongo();


        using (Session = await MongoClient.StartSessionAsync())
        {
            Session.StartTransaction();

            var commandTasks = _commands.Select(c => c());

            await Task.WhenAll(commandTasks);

            await Session.CommitTransactionAsync();
        }

        return _commands.Count;
    }

    private void ConfigureMongo()
    {
        if (MongoClient != null)
        {
            return;
        }

        MongoClient = new MongoClient(_configuration.GetConnectionString("DefaultConnection"));

        Database = MongoClient.GetDatabase("ShortenerStore");
    }

    public IMongoCollection<T> GetCollection<T>(string name)
    {
        ConfigureMongo();

        return Database.GetCollection<T>(name);
    }

    public void Dispose()
    {
        Session?.Dispose();
        GC.SuppressFinalize(this);
    }

    public void AddCommand(Func<Task> func)
    {
        _commands.Add(func);
    }

    public async Task<bool> Commit()
    {
        return await SaveChanges() > 0;
    }
}