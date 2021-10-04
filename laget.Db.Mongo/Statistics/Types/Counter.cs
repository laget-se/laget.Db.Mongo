using System;
using MongoDB.Bson.Serialization.Attributes;

namespace laget.Db.Mongo.Statistics.Types
{
    public class Counter : Entity
    {
        [BsonElement("count")]
        public int Count { get; set; } = 1;

        [BsonElement("date"), BsonDateTimeOptions(DateOnly = true)]
        public DateTime Date { get; set; } = DateTime.Now.Date;
    }
}
