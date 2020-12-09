using System;
using System.Collections.Generic;
using laget.Db.Mongo.Extensions;
using Xunit;

namespace laget.Db.Mongo.Tests.Extensions
{
    public class BsonCollectionAttributeTests
    {
        [Fact]
        public void IsAttributeMultipleFalse()
        {
            var attributes = (IList<AttributeUsageAttribute>)typeof(BsonCollectionAttribute).GetCustomAttributes(typeof(AttributeUsageAttribute), false);
            Assert.Equal(1, attributes.Count);

            var attribute = attributes[0];
            Assert.False(attribute.AllowMultiple);
        }

        [Fact]
        public void ShouldReturnCorrectTableName()
        {
            const string expected = "entries";
            var actual = new TestClass().CollectionName;

            Assert.Equal(expected, actual);
        }


        [BsonCollection("entries")]

        public class TestClass : Entity
        {
            public string CollectionName
            {
                get
                {
                    var attribute = (BsonCollectionAttribute)Attribute.GetCustomAttribute(typeof(TestClass), typeof(BsonCollectionAttribute));

                    return attribute == null ? nameof(TestClass) : attribute.CollectionName;
                }
            }
        }
    }
}
