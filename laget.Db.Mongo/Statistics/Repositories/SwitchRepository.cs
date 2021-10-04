using System.Collections.Generic;
using laget.Db.Mongo.Statistics.Types;
using MongoDB.Driver;

namespace laget.Db.Mongo.Statistics.Repositories
{
    public class SwitchRepository<T> : Repository<T> where T : Switch
    {
        public SwitchRepository(IMongoStatisticsProvider provider)
            : base(provider)
        {
        }

        public IEnumerable<T> List()
        {
            var builder = Builders<T>.Filter;

            var filter = builder.Eq(x => x.Name, typeof(T).Name);

            return Find(filter);
        }

        public void Set(T entry)
        {
            var builder = Builders<T>.Filter;

            var filter = builder.Eq(x => x.Name, typeof(T).Name) &
                         builder.Eq(x => x.DateTime, entry.DateTime);

            Upsert(filter, entry);
        }

        public void Set(string name, bool value)
        {
            var entry = (T)System.Activator.CreateInstance(typeof(T));
            entry.Name = name;
            entry.On = value;

            var builder = Builders<T>.Filter;

            var filter = builder.Eq(x => x.Name, typeof(T).Name) &
                         builder.Eq(x => x.DateTime, entry.DateTime);

            Upsert(filter, entry);
        }
    }
}
