using MongoDB.Driver;

namespace Newbody.Integration.Db.Mongo
{
    public interface IMongoDefaultProvider
    {
        IMongoDatabase GetDatabase();
        IMongoCollection<T> GetCollection<T>(string name);
    }

    public class MongoDefaultProvider : IMongoDefaultProvider
    {
        readonly IMongoDatabase _database;

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

            _database = client.GetDatabase(url.DatabaseName, settings);
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
