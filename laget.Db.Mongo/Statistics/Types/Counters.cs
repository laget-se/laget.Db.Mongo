using System;
using MongoDB.Bson.Serialization.Attributes;

namespace laget.Db.Mongo.Statistics.Types
{
    public class Counters : Entity
    {
        [BsonElement("date"), BsonDateTimeOptions(DateOnly = true)]
        public DateTime Date { get; set; } = DateTime.Now.Date;
    }
}
