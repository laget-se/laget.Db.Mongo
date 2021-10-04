namespace laget.Db.Mongo.Statistics.Repositories
{
    public class ObjectRepository<T> : Repository<T> where T : Entity
    {
        public ObjectRepository(IMongoStatisticsProvider provider)
            : base(provider)
        {
        }
    }
}
