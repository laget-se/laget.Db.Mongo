using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;

namespace laget.Db.Mongo
{
    public class CachedRepository<TEntity> : Repository<TEntity>, IRepository<TEntity> where TEntity : Entity
    {
        public CachedRepository(IMongoDefaultProvider provider)
            : base(provider)
        {
        }

        public override IEnumerable<TEntity> Find(FilterDefinition<TEntity> filter)
        {
            var cacheKey = $"{CachePrefix}_{filter}";

            return Cache.GetOrCreate(cacheKey, entry => base.Find(filter));
        }

        public override async Task<IEnumerable<TEntity>> FindAsync(FilterDefinition<TEntity> filter)
        {
            var cacheKey = $"{CachePrefix}_{filter}";

            return await Cache.GetOrCreate(cacheKey, entry => base.FindAsync(filter));
        }

        public override TEntity Get(string id)
        {
            var cacheKey = $"{CachePrefix}_{id}";

            return Cache.GetOrCreate(cacheKey, entry => base.Get(id));
        }

        public override async Task<TEntity> GetAsync(string id)
        {
            var cacheKey = $"{CachePrefix}_{id}";

            return await Cache.GetOrCreate(cacheKey, entry => base.GetAsync(id));
        }

        public override TEntity Get(FilterDefinition<TEntity> filter)
        {
            var cacheKey = $"{CachePrefix}_{filter}";

            return Cache.GetOrCreate(cacheKey, entry => base.Get(filter));
        }

        public override async Task<TEntity> GetAsync(FilterDefinition<TEntity> filter)
        {
            var cacheKey = $"{CachePrefix}_{filter}";

            return await Cache.GetOrCreate(cacheKey, entry => base.GetAsync(filter));
        }

        public override void Insert(IEnumerable<TEntity> entities)
        {
        }

        public override async Task InsertAsync(IEnumerable<TEntity> entities)
        {
        }

        public override void Insert(TEntity entity)
        {
            var cacheKey = $"{CachePrefix}_Id_{entity.Id}";

            base.Insert(entity);

            Cache.Set(cacheKey, entity);
        }

        public override async Task InsertAsync(TEntity entity)
        {
            var cacheKey = $"{CachePrefix}_Id_{entity.Id}";

            await base.InsertAsync(entity);

            Cache.Set(cacheKey, entity);
        }

        public override void Update(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, UpdateOptions options)
        {
        }

        public override async Task UpdateAsync(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, UpdateOptions options)
        {
        }

        public override void Upsert(IEnumerable<TEntity> entities)
        {
        }

        public override async Task UpsertAsync(IEnumerable<TEntity> entities)
        {
        }

        public override void Upsert(TEntity entity)
        {
        }

        public override async Task UpsertAsync(TEntity entity)
        {
        }

        public override void Upsert(FilterDefinition<TEntity> filter, TEntity entity)
        {
        }

        public override async Task UpsertAsync(FilterDefinition<TEntity> filter, TEntity entity)
        {
        }

        public override void Delete(IEnumerable<TEntity> entities)
        {
        }

        public override async Task DeleteAsync(IEnumerable<TEntity> entities)
        {
        }

        public override void Delete(TEntity entity)
        {
        }

        public override async Task DeleteAsync(TEntity entity)
        {
        }

        public override void Delete(FilterDefinition<TEntity> filter)
        {
        }

        public override async Task DeleteAsync(FilterDefinition<TEntity> filter)
        {
        }
    }
}
