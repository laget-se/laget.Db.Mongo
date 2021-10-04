using MongoDB.Driver;

namespace laget.Db.Mongo.Statistics
{
    public interface IMongoStatisticsProvider : IMongoDefaultProvider
    {
    }

    public class MongoStatisticsProvider : MongoDefaultProvider, IMongoStatisticsProvider
    {
        public MongoStatisticsProvider(string connectionString)
            : base(connectionString, new MongoDatabaseSettings
            {
                ReadConcern = ReadConcern.Default,
                ReadPreference = ReadPreference.SecondaryPreferred,
                WriteConcern = WriteConcern.W1
            })
        {
        }
    }
}
