# laget.Db.Mongo


## Usage
```c#
public interface IUserRepository : IRepository<Models.User>
{
}

public class UserRepository : Repository<Models.User>, IUserRepository
{
    public UserRepository(string connectionString)
        : base(connectionString, new Cache(nameof(UserRepository)))
    {
    }
}
```
