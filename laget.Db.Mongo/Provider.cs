using System;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;

namespace laget.Db.Mongo
{
    public interface IMongoDefaultProvider
    {
        MemoryCacheOptions CacheOptions { get; }
        IMongoDatabase GetDatabase();
        IMongoCollection<T> GetCollection<T>(string name);
    }

    public class MongoDefaultProvider : IMongoDefaultProvider
    {
        public MemoryCacheOptions CacheOptions { get; }
        private readonly IMongoDatabase _database;

        public MongoDefaultProvider(string connectionString)
            : this(connectionString, new MongoDatabaseSettings
            {
                ReadConcern = ReadConcern.Default,
                ReadPreference = ReadPreference.SecondaryPreferred,
                WriteConcern = WriteConcern.W3
            }, new MemoryCacheOptions
            {
                ExpirationScanFrequency = TimeSpan.FromMinutes(1)
            })
        {
        }

        public MongoDefaultProvider(string connectionString, MongoDatabaseSettings settings)
            : this(connectionString, settings, new MemoryCacheOptions
            {
                ExpirationScanFrequency = TimeSpan.FromMinutes(1)
            })
        {
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

            _database = client.GetDatabase(url.DatabaseName, settings);
            CacheOptions = cacheOptions;
        }

        public IMongoDatabase GetDatabase()
        {
            return _database;
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _database.GetCollection<T>(name);
        }
    }
}
