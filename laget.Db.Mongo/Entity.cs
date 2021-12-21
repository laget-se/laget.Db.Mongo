using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace laget.Db.Mongo
{
    public class Entity
    {
        [BsonElement("id"), BsonId, BsonIgnoreIfDefault, BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }


        [BsonElement("createdAt"), BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public virtual DateTime CreatedAt { get; set; } = DateTime.Now;
        [BsonElement("updatedAt"), BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public virtual DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
