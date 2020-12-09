using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;

namespace laget.Db.Mongo
{
    public class CacheableRepository<TEntity> : Repository<TEntity>, IRepository<TEntity> where TEntity : Entity
    {
        protected readonly IMemoryCache Cache;
        protected readonly Cache Options;

        public CacheableRepository(IMongoDefaultProvider provider, Cache cache)
            : base(provider)
        {
            Cache = new MemoryCache(new MemoryCacheOptions
            {
                ExpirationScanFrequency = TimeSpan.FromMinutes(1)
            });
            Options = cache;
        }


        public override IEnumerable<TEntity> Find(FilterDefinition<TEntity> filter)
        {
            var item = CacheGet<IEnumerable<TEntity>>(filter.ToString());
            if (item != null)
            {
                return item;
            }

            var result = base.Find(filter);

            CacheAdd(filter.ToString(), result);

            return result;
        }

        public override async Task<IEnumerable<TEntity>> FindAsync(FilterDefinition<TEntity> filter)
        {
            var item = CacheGet<IEnumerable<TEntity>>(filter.ToString());
            if (item != null)
            {
                return await Task.FromResult(item);
            }

            var result = base.FindAsync(filter.ToString());

            CacheAdd(filter.ToString(), result.Result);

            return await result;
        }

        public override TEntity Get(string id)
        {
            var item = CacheGet<TEntity>(id);
            if (item != null)
            {
                return item;
            }

            var result = base.Get(id);

            CacheAdd(id, result);

            return result;
        }

        public override async Task<TEntity> GetAsync(string id)
        {
            var item = CacheGet<TEntity>(id);
            if (item != null)
            {
                return await Task.FromResult(item);
            }

            var result = base.GetAsync(id);

            CacheAdd(id, result.Result);

            return await result;
        }

        public override TEntity Get(FilterDefinition<TEntity> filter)
        {
            var item = CacheGet<TEntity>(filter.ToString());
            if (item != null)
            {
                return item;
            }

            var result = base.Get(filter);

            CacheAdd(filter.ToString(), result);

            return result;
        }

        public override async Task<TEntity> GetAsync(FilterDefinition<TEntity> filter)
        {
            var item = CacheGet<TEntity>(filter.ToString());
            if (item != null)
            {
                return await Task.FromResult(item);
            }

            var result = base.GetAsync(filter.ToString());

            CacheAdd(filter.ToString(), result.Result);

            return await result;
        }

        //public void Insert(IEnumerable<TEntity> entities)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public async Task InsertAsync(IEnumerable<TEntity> entities)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public void Insert(TEntity entity)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public async Task InsertAsync(TEntity entity)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public void Update(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, UpdateOptions options)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public async Task UpdateAsync(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, UpdateOptions options)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public void Upsert(IEnumerable<TEntity> entities)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public async Task UpsertAsync(IEnumerable<TEntity> entities)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public void Upsert(TEntity entity)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public async Task UpsertAsync(TEntity entity)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public void Upsert(FilterDefinition<TEntity> filter, TEntity entity)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public async Task UpsertAsync(FilterDefinition<TEntity> filter, TEntity entity)
        //{
        //    throw new System.NotImplementedException();
        //}


        protected TZ CacheGet<TZ>(string key)
        {
            return Cache.Get<TZ>($"{Options.KeyPrefix}_{key}");
        }

        protected void CacheAdd<TZ>(string key, TZ item)
        {
            Cache.Set($"{Options.KeyPrefix}_{key}", item,
                new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(15),
                    AbsoluteExpirationRelativeToNow = Options.Expiration,
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = Options.Expiration
                });
        }
    }
}
