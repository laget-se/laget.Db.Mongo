using System.Collections.Generic;
using laget.Db.Mongo.Statistics.Types;
using MongoDB.Driver;

namespace laget.Db.Mongo.Statistics.Repositories
{
    public class CounterRepository<T> : Repository<T> where T : Counter
    {
        public CounterRepository(IMongoStatisticsProvider provider)
            : base(provider)
        {
        }

        public void Increase(FilterDefinition<T> filter)
        {
            var options = new UpdateOptions();
            var builder = Builders<T>.Update;

            var updates = new List<UpdateDefinition<T>>
            {
                builder.Inc(p => p.Count, 1)
            };

            Update(filter, builder.Combine(updates), options);
        }

        public void Increase(FilterDefinition<T> filter, int count)
        {
            var options = new UpdateOptions();
            var builder = Builders<T>.Update;

            var updates = new List<UpdateDefinition<T>>
            {
                builder.Inc(p => p.Count, count)
            };

            Update(filter, builder.Combine(updates), options);
        }

        public void Decrease(FilterDefinition<T> filter)
        {
            var options = new UpdateOptions();
            var builder = Builders<T>.Update;

            var updates = new List<UpdateDefinition<T>>
            {
                builder.Inc(p => p.Count, -1)
            };

            Update(filter, builder.Combine(updates), options);
        }

        public void Decrease(FilterDefinition<T> filter, int count)
        {
            var options = new UpdateOptions();
            var builder = Builders<T>.Update;

            var updates = new List<UpdateDefinition<T>>
            {
                builder.Inc(p => p.Count, count)
            };

            Update(filter, builder.Combine(updates), options);
        }

        public bool Exists(FilterDefinition<T> filter)
        {
            return Collection.CountDocuments(filter) > 0;
        }
    }
}
