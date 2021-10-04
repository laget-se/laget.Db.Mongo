using System;
using MongoDB.Bson.Serialization.Attributes;

namespace laget.Db.Mongo.Statistics.Types
{
    public class Object : Entity
    {
        [BsonElement("data")]
        public object Data { get; set; }

        [BsonElement("date"),BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
