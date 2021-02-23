# laget.Db.Mongo
A generic implementation of MongoDB, a cross-platform document-oriented database program. Classified as a NoSQL database program, MongoDB uses JSON-like documents with optional schemas.

![Nuget](https://img.shields.io/nuget/v/laget.Db.Mongo)
![Nuget](https://img.shields.io/nuget/dt/laget.Db.Mongo)

## Configuration
> This example is shown using Autofac since this is the go-to IoC for us.
```c#
public class DatabaseModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.Register(c => new MongoDefaultProvider(c.Resolve<IConfiguration>().GetConnectionString("MongoConnectionString"))).As<IMongoDefaultProvider>().SingleInstance();
    }
}
```
```c#
public class DatabaseModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.Register(c => new MongoDefaultProvider(c.Resolve<IConfiguration>().GetConnectionString("MongoConnectionString"), new MongoDatabaseSettings
            {
                ReadConcern = ReadConcern.Default,
                ReadPreference = ReadPreference.SecondaryPreferred,
                WriteConcern = WriteConcern.W3
            })).As<IMongoDefaultProvider>().SingleInstance();
    }
}
```
```c#
public class DatabaseModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.Register(c => new MongoDefaultProvider(c.Resolve<IConfiguration>().GetConnectionString("MongoConnectionString"), new MemoryCacheOptions
            {
                CompactionPercentage = 0.25,
                ExpirationScanFrequency = TimeSpan.FromMinutes(5),
                SizeLimit = 1024
            })).As<IMongoDefaultProvider>().SingleInstance();
    }
}
```
```c#
public class DatabaseModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.Register(c => new MongoDefaultProvider(c.Resolve<IConfiguration>().GetConnectionString("MongoConnectionString"), new MongoDatabaseSettings
            {
                ReadConcern = ReadConcern.Default,
                ReadPreference = ReadPreference.SecondaryPreferred,
                WriteConcern = WriteConcern.W3
            }, new MemoryCacheOptions
            {
                CompactionPercentage = 0.25,
                ExpirationScanFrequency = TimeSpan.FromMinutes(5),
                SizeLimit = 1024
            })).As<IMongoDefaultProvider>().SingleInstance();
    }
}
```

## Usage
#### Built-in methods
```c#
public interface IRepository<TEntity> where TEntity : Entity
{
    IEnumerable<TEntity> Find(FilterDefinition<TEntity> filter);
    Task<IEnumerable<TEntity>> FindAsync(FilterDefinition<TEntity> filter);

    TEntity Get(string id);
    Task<TEntity> GetAsync(string id);
    TEntity Get(FilterDefinition<TEntity> filter);
    Task<TEntity> GetAsync(FilterDefinition<TEntity> filter);

    void Insert(IEnumerable<TEntity> entities);
    Task InsertAsync(IEnumerable<TEntity> entities);
    void Insert(TEntity entity);
    Task InsertAsync(TEntity entity);

    void Update(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, UpdateOptions options);
    Task UpdateAsync(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, UpdateOptions options);

    void Upsert(IEnumerable<TEntity> entities);
    Task UpsertAsync(IEnumerable<TEntity> entities);
    void Upsert(TEntity entity);
    Task UpsertAsync(TEntity entity);
    void Upsert(FilterDefinition<TEntity> filter, TEntity entity);
    Task UpsertAsync(FilterDefinition<TEntity> filter, TEntity entity);

    void Delete(IEnumerable<TEntity> entities);
    Task DeleteAsync(IEnumerable<TEntity> entities);
    void Delete(TEntity entity);
    Task DeleteAsync(TEntity entity);
    void Delete(FilterDefinition<TEntity> filter);
    Task DeleteAsync(FilterDefinition<TEntity> filter);
}
```

### Generic
```c#
public interface IUserRepository : IRepository<Models.User>
{
}

public class UserRepository : Repository<Models.User>, IUserRepository
{
    public UserRepository(string connectionString)
        : base(connectionString)
    {
    }
}
```

### Caching
```c#
public interface IUserRepository : IRepository<Models.User>
{
}

public class UserRepository : Repository<Models.User>, IUserRepository
{
    public UserRepository(string connectionString)
        : base(connectionString)
    {
    }

    public override Models.User Get(string id)
    {
        var cacheKey = "_Id_" + id;
        var item = CacheGet<Models.User>(cacheKey);

        if (item != null)
            return item;

        var result = Get(id);

        CacheAdd(cacheKey, result);

        return result;
    }
}
```
