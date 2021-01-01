using System;

namespace laget.Db.Mongo.Tests.Fixtures
{
    public class ReadOnlyRepositoryFixture<TEntity> : IDisposable where TEntity : Entity
    {
        public ReadOnlyTestRepository<TEntity> Repository { get; private set; }

        public ReadOnlyRepositoryFixture()
        {
            Repository = new ReadOnlyTestRepository<TEntity>();
        }

        public void Dispose()
        {
            Repository = null;
        }
    }

    public interface IReadOnlyTestRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
    }

    public class ReadOnlyTestRepository<TEntity> : ReadOnlyRepository<TEntity>, IReadOnlyTestRepository<TEntity> where TEntity : Entity
    {
        public ReadOnlyTestRepository() : base(new MongoDefaultProvider("mongodb://127.0.0.1:27017/test")) { }
    }
}
