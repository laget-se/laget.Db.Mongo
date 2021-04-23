using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using laget.Db.Mongo.Extensions;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;

namespace laget.Db.Mongo
{
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
        void UpdateMany(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, UpdateOptions options);
        Task UpdateManyAsync(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, UpdateOptions options);

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

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly IMemoryCache Cache;
        protected readonly IMongoCollection<TEntity> Collection;

        protected string CachePrefix => GetCachePrefix();

        public Repository(IMongoDefaultProvider provider)
        {
            Collection = provider.Collection<TEntity>(GetCollectionName());
            Cache = new MemoryCache(provider.CacheOptions);
        }

        public virtual IEnumerable<TEntity> Find(FilterDefinition<TEntity> filter)
        {
            return Collection.Find(filter).ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> FindAsync(FilterDefinition<TEntity> filter)
        {
            return await Collection.Find(filter).ToListAsync();
        }

        public virtual TEntity Get(string id)
        {
            var filter = Builders<TEntity>.Filter.Eq(x => x.Id, id);

            return Collection.Find(filter).FirstOrDefault();
        }

        public virtual async Task<TEntity> GetAsync(string id)
        {
            var filter = Builders<TEntity>.Filter.Eq(x => x.Id, id);

            return await Collection.Find(filter).FirstOrDefaultAsync();
        }

        public virtual TEntity Get(FilterDefinition<TEntity> filter)
        {
            return Collection.Find(filter).FirstOrDefault();
        }

        public virtual async Task<TEntity> GetAsync(FilterDefinition<TEntity> filter)
        {
            return await Collection.Find(filter).FirstOrDefaultAsync();
        }

        public virtual void Insert(IEnumerable<TEntity> entities)
        {
            Collection.InsertMany(entities);
        }

        public virtual async Task InsertAsync(IEnumerable<TEntity> entities)
        {
            await Collection.InsertManyAsync(entities);
        }

        public virtual void Insert(TEntity entity)
        {
            Collection.InsertOne(entity);
        }

        public virtual async Task InsertAsync(TEntity entity)
        {
            await Collection.InsertOneAsync(entity);
        }

        public virtual void Update(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, UpdateOptions options)
        {
            Collection.UpdateOne(filter, update, options);
        }

        public virtual async Task UpdateAsync(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, UpdateOptions options)
        {
            await Collection.UpdateOneAsync(filter, update, options);
        }

        public virtual void UpdateMany(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, UpdateOptions options)
        {
            Collection.UpdateMany(filter, update, options);
        }

        public virtual async Task UpdateManyAsync(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, UpdateOptions options)
        {
            await Collection.UpdateManyAsync(filter, update, options);
        }

        public virtual void Upsert(IEnumerable<TEntity> entities)
        {
            var options = new ReplaceOptions { IsUpsert = true };

            foreach (var entity in entities)
            {
                var filter = Builders<TEntity>.Filter.Eq(x => x.Id, entity.Id);

                Collection.ReplaceOne(filter, entity, options);
            }
        }

        public virtual async Task UpsertAsync(IEnumerable<TEntity> entities)
        {
            var options = new ReplaceOptions { IsUpsert = true };

            foreach (var entity in entities)
            {
                var filter = Builders<TEntity>.Filter.Eq(x => x.Id, entity.Id);

                await Collection.ReplaceOneAsync(filter, entity, options);
            }
        }

        public virtual void Upsert(TEntity entity)
        {
            var options = new ReplaceOptions { IsUpsert = true };
            var filter = Builders<TEntity>.Filter.Eq(x => x.Id, entity.Id);

            Collection.ReplaceOne(filter, entity, options);
        }

        public virtual async Task UpsertAsync(TEntity entity)
        {
            var options = new ReplaceOptions { IsUpsert = true };
            var filter = Builders<TEntity>.Filter.Eq(x => x.Id, entity.Id);

            await Collection.ReplaceOneAsync(filter, entity, options);
        }

        public virtual void Upsert(FilterDefinition<TEntity> filter, TEntity entity)
        {
            var options = new ReplaceOptions { IsUpsert = true };

            Collection.ReplaceOne(filter, entity, options);
        }

        public virtual async Task UpsertAsync(FilterDefinition<TEntity> filter, TEntity entity)
        {
            var options = new ReplaceOptions { IsUpsert = true };

            await Collection.ReplaceOneAsync(filter, entity, options);
        }

        public virtual void Delete(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                var filter = Builders<TEntity>.Filter.Eq(x => x.Id, entity.Id);

                Collection.DeleteOne(filter);
            }
        }

        public virtual async Task DeleteAsync(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                var filter = Builders<TEntity>.Filter.Eq(x => x.Id, entity.Id);

                await Collection.DeleteOneAsync(filter);
            }
        }

        public virtual void Delete(TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq(x => x.Id, entity.Id);

            Collection.DeleteOne(filter);
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq(x => x.Id, entity.Id);

            await Collection.DeleteOneAsync(filter);
        }

        public virtual void Delete(FilterDefinition<TEntity> filter)
        {
            Collection.DeleteMany(filter);
        }

        public virtual async Task DeleteAsync(FilterDefinition<TEntity> filter)
        {
            await Collection.DeleteManyAsync(filter);
        }


        protected TZ CacheGet<TZ>(string key)
        {
            return Cache.Get<TZ>($"{CachePrefix}_{key}");
        }

        protected void CacheAdd<TZ>(string key, TZ item, MemoryCacheEntryOptions options = null)
        {
            if (options == null)
            {
                options = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(15),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                };
            }

            Cache.Set($"{CachePrefix}_{key}", item, options);
        }


        private static string GetCachePrefix()
        {
            var attribute = (BsonCollectionAttribute)Attribute.GetCustomAttribute(typeof(TEntity), typeof(BsonCollectionAttribute));

            return attribute == null ? typeof(TEntity).Name : attribute.CachePrefix;
        }

        private static string GetCollectionName()
        {
            var attribute = (BsonCollectionAttribute)Attribute.GetCustomAttribute(typeof(TEntity), typeof(BsonCollectionAttribute));

            return attribute == null ? typeof(TEntity).Name.ToLower() : attribute.CollectionName;
        }
    }
}
