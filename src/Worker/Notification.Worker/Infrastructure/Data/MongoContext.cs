using MongoDB.Driver;
using Notification.Core.Common.CQRS;
using Notification.Core.MessageBus.Services.Interfaces;
using Notification.Worker.Data.Interfaces;
using Polly;
using RabbitMQ.Client.Exceptions;

namespace Notification.Worker.Data;

public class MongoContext : IMongoContext
{
    private IMongoDatabase Database { get; set; }
    public IClientSessionHandle Session { get; set; }
    public MongoClient MongoClient { get; set; }
    private readonly List<Func<Task>> _commands;
    private readonly IConfiguration _configuration;
    private readonly IMessageBus _messageBus;
    private readonly List<AggregateRoot> _modifiedAggregates = new List<AggregateRoot>();

    
    public MongoContext(IConfiguration configuration, IMessageBus messageBus)
    {
        _configuration = configuration;

        _commands = new List<Func<Task>>();

        _messageBus = messageBus;
    }

    public async Task<int> SaveChanges()
    { 
        ConfigureMongo();

        var circuitBreakerPolicy = Policy
            .Handle<MongoConnectionException>()
            .Or<System.TimeoutException>() 
            .CircuitBreakerAsync(3, TimeSpan.FromSeconds(30), (ex, breakDuration) =>
            {
                Console.WriteLine($"[MONGO CONTEXT][CIRCUIT BROKEN] - Circuit broken due to {ex.GetType().Name}. Will remain open for {breakDuration.TotalSeconds} seconds.");
            }, () =>
            {
                Console.WriteLine("[MONGO CONTEXT][CIRCUIT BROKEN] - Circuit reset.");
            });

        var retryPolicy = Policy
            .Handle<MongoConnectionException>()
            .Or<System.TimeoutException>()
            .WaitAndRetryAsync(6, op => TimeSpan.FromSeconds(Math.Pow(2, op)), (ex, time) =>
            {
                Console.WriteLine($"[MONGO CONTEXT][RETRY] - Retry operation after failure: {ex.GetType().Name}");
            });

        var policyWrap = Policy.WrapAsync(circuitBreakerPolicy, retryPolicy);
        
        await policyWrap.ExecuteAsync(async () =>
        {
            using (Session = await MongoClient.StartSessionAsync())
            {
                Session.StartTransaction();

                var commandTasks = _commands.Select(c => c());

                await Task.WhenAll(commandTasks);

                await Session.CommitTransactionAsync();
            }
        });

        return _commands.Count;
    }

    private void ConfigureMongo()
    {
        if (MongoClient != null)
        {
            return;
        }

        MongoClient = new MongoClient(_configuration.GetConnectionString("DefaultConnection"));

        Database = MongoClient.GetDatabase("NotificationDB");
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

    public void AddCommand(Func<Task> func, AggregateRoot? aggregateRoot = null)
    {
        if (aggregateRoot is not null)
            MarkAsModified(aggregateRoot);

        _commands.Add(func);
    }

    protected void MarkAsModified(AggregateRoot entity)
    {
        if (!_modifiedAggregates.Contains(entity))
            _modifiedAggregates.Add(entity);

    }
    
    public async Task Commit()
    {
        if (await SaveChanges() < 0)
            throw new OperationCanceledException("Unable to persist changes to database");

        var events = _modifiedAggregates.SelectMany(c => c.Events).ToList();
        
        _modifiedAggregates.ForEach(c => c.Clear());
        _modifiedAggregates.Clear();
        
        foreach (var @event in events)
        {
            _messageBus.Publish(@event.Exchange, @event.RouterKey, @event);
        } 
    }
}