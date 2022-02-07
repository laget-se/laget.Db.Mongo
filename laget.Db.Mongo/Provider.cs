using System;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;

namespace laget.Db.Mongo
{
    public interface IMongoDefaultProvider
    {
        MemoryCacheOptions CacheOptions { get; }
        IMongoDatabase Database { get; }
        IMongoCollection<T> Collection<T>(string name);
    }

    public class MongoDefaultProvider : IMongoDefaultProvider
    {
        public MemoryCacheOptions CacheOptions { get; }
        public IMongoDatabase Database { get; }

        public MongoDefaultProvider(string connectionString)
            : this(connectionString, new MongoDatabaseSettings
            {
                ReadConcern = ReadConcern.Default,
                ReadPreference = ReadPreference.SecondaryPreferred,
                WriteConcern = WriteConcern.W3
            }, new MemoryCacheOptions
            {
                ExpirationScanFrequency = TimeSpan.FromMinutes(5)
            })
        {
        }

        public MongoDefaultProvider(string connectionString, MongoDatabaseSettings settings)
        {
            var url = new MongoUrl(connectionString);
            var client = new MongoClient(url);

            Database = client.GetDatabase(url.DatabaseName, settings);
        }

        public MongoDefaultProvider(string connectionString, MemoryCacheOptions cacheOptions)
            : this(connectionString, new MongoDatabaseSettings
            {
                ReadConcern = ReadConcern.Default,
                ReadPreference = ReadPreference.SecondaryPreferred,
                WriteConcern = WriteConcern.W3
            }, cacheOptions)
        {
        }

        public MongoDefaultProvider(string connectionString, MongoDatabaseSettings settings, MemoryCacheOptions cacheOptions)
        {
            var url = new MongoUrl(connectionString);
            var client = new MongoClient(url);

            CacheOptions = cacheOptions;
            Database = client.GetDatabase(url.DatabaseName, settings);
        }

        public IMongoCollection<T> Collection<T>(string name) => Database.GetCollection<T>(name);
    }
}
