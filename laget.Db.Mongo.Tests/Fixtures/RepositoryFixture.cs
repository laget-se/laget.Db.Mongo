using System;

namespace Newbody.Integration.Db.Mongo.Tests.Fixtures
{
    public class RepositoryFixture<TEntity> : IDisposable where TEntity : Entity
    {
        public TestRepository<TEntity> Repository { get; private set; }

        public RepositoryFixture()
        {
            Repository = new TestRepository<TEntity>();
        }

        public void Dispose()
        {
            Repository = null;
        }
    }

    public interface ITestRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
    }

    public class TestRepository<TEntity> : Repository<TEntity>, ITestRepository<TEntity> where TEntity : Entity
    {
        public TestRepository() : base(new MongoDefaultProvider(string.Empty)) { }
    }
}
