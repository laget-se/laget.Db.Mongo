using System;
using MongoDB.Driver;

namespace laget.Db.Mongo
{
    public interface IMongoDefaultProvider
    {
        IMongoDatabase Database { get; }
        IMongoCollection<T> Collection<T>(string name);
    }

    public class MongoDefaultProvider : IMongoDefaultProvider
    {
        public IMongoDatabase Database { get; }

        public MongoDefaultProvider(string connectionString)
            : this(connectionString, new MongoDatabaseSettings
            {
                ReadConcern = ReadConcern.Default,
                ReadPreference = ReadPreference.SecondaryPreferred,
                WriteConcern = WriteConcern.W3
            })
        {
        }

        public MongoDefaultProvider(string connectionString, MongoDatabaseSettings settings)
        {
            var url = new MongoUrl(connectionString);
            var client = new MongoClient(url);

            Database = client.GetDatabase(url.DatabaseName, settings);
        }

        public IMongoCollection<T> Collection<T>(string name) => Database.GetCollection<T>(name);
    }
}
