# laget.Db.Mongo
A generic implementation of MongoDB, a cross-platform document-oriented database program. Classified as a NoSQL database program, MongoDB uses JSON-like documents with optional schemas.

![Nuget](https://img.shields.io/nuget/v/laget.Db.Mongo)
![Nuget](https://img.shields.io/nuget/dt/laget.Db.Mongo)

## Usage
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
