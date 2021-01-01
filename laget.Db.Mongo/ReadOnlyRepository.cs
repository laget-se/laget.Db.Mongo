using System.Collections.Generic;
using System.Threading.Tasks;
using laget.Db.Mongo.Exceptions;
using MongoDB.Driver;

namespace laget.Db.Mongo
{
    public class ReadOnlyRepository<TEntity> : Repository<TEntity>, IRepository<TEntity> where TEntity : Entity
    {
        public ReadOnlyRepository(IMongoDefaultProvider provider)
            : base(provider)
        {
        }

        public override void Insert(IEnumerable<TEntity> entities)
        {
            throw new ReadOnlyException();
        }

        public override Task InsertAsync(IEnumerable<TEntity> entities)
        {
            throw new ReadOnlyException();
        }

        public override void Insert(TEntity entity)
        {
            throw new ReadOnlyException(entity.GetType());
        }

        public override Task InsertAsync(TEntity entity)
        {
            throw new ReadOnlyException(entity.GetType());
        }

        public override void Update(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, UpdateOptions options)
        {
            throw new ReadOnlyException();
        }

        public override Task UpdateAsync(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, UpdateOptions options)
        {
            throw new ReadOnlyException();
        }

        public override void Upsert(IEnumerable<TEntity> entities)
        {
            throw new ReadOnlyException();
        }

        public override Task UpsertAsync(IEnumerable<TEntity> entities)
        {
            throw new ReadOnlyException();
        }

        public override void Upsert(TEntity entity)
        {
            throw new ReadOnlyException(entity.GetType());
        }

        public override Task UpsertAsync(TEntity entity)
        {
            throw new ReadOnlyException(entity.GetType());
        }

        public override void Upsert(FilterDefinition<TEntity> filter, TEntity entity)
        {
            throw new ReadOnlyException(entity.GetType());
        }

        public override Task UpsertAsync(FilterDefinition<TEntity> filter, TEntity entity)
        {
            throw new ReadOnlyException(entity.GetType());
        }

        public override void Delete(IEnumerable<TEntity> entities)
        {
            throw new ReadOnlyException();
        }

        public override Task DeleteAsync(IEnumerable<TEntity> entities)
        {
            throw new ReadOnlyException();
        }

        public override void Delete(TEntity entity)
        {
            throw new ReadOnlyException(entity.GetType());
        }

        public override Task DeleteAsync(TEntity entity)
        {
            throw new ReadOnlyException(entity.GetType());
        }

        public override void Delete(FilterDefinition<TEntity> filter)
        {
            throw new ReadOnlyException();
        }

        public override Task DeleteAsync(FilterDefinition<TEntity> filter)
        {
            throw new ReadOnlyException();
        }
    }
}
