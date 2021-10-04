using System;
using MongoDB.Bson.Serialization.Attributes;

namespace laget.Db.Mongo.Statistics.Types
{
    public class Switch : Entity
    {
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("on")]
        public bool On { get; set; } = true;

        [BsonElement("date"), BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}
