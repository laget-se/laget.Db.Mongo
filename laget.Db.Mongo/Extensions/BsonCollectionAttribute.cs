using System;

namespace laget.Db.Mongo.Extensions
{
    [AttributeUsage(AttributeTargets.Class)]
    public class BsonCollectionAttribute : Attribute
    {
        public virtual string CollectionName { get; }
        public virtual string CachePrefix { get; }

        public BsonCollectionAttribute(string collectionName)
        {
            CollectionName = collectionName;
        }

        public BsonCollectionAttribute(string collectionName, string cachePrefix)
        {
            CollectionName = collectionName;
            CachePrefix = cachePrefix;
        }
    }
}
