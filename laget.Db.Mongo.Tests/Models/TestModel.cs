using System;
using laget.Db.Mongo.Extensions;
using MongoDB.Bson;

namespace laget.Db.Mongo.Tests.Models
{
    [BsonCollection("tests")]
    public class TestModel : Entity
    {
        public string Field1 { get; set; } = "Lorem ipsum dolor sit amet";
        public bool Field2 { get; set; } = true;
        public DateTime Field3 { get; set; } = DateTime.Now;

        public TestModel()
        {
            Id = ObjectId.Parse("507f1f77bcf86cd799439011").ToString();
            CreatedAt = DateTime.Now.AddMonths(-1);
            UpdatedAt = DateTime.Now;
        }
    }
}
