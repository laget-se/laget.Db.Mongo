using System;
using Moq;
using Xunit;

namespace laget.Db.Mongo.Tests
{
    public class ProviderTests
    {
        private static string ConnectionString => "mongodb://myDBReader:D1fficultP%40ssw0rd@mongodb0.example.com:27017/database?authSource=admin";

        [Fact]
        public void ShouldReturnCorrectDatabaseName()
        {
            var provider = new Mock<MongoDefaultProvider>(ConnectionString).Object;

            const string expected = "database";
            var actual = provider.Database;

            Assert.Equal(expected, actual.DatabaseNamespace.DatabaseName);
        }

        [Fact]
        public void ShouldReturnCorrectCollection()
        {
            var provider = new Mock<MongoDefaultProvider>(ConnectionString).Object;

            const string expected = "collection";
            var actual = provider.Collection<Models.TestModel>("collection");

            Assert.Equal(expected, actual.CollectionNamespace.CollectionName);
            Assert.Equal("database.collection", actual.CollectionNamespace.FullName);
        }

        [Fact]
        public void ShouldReturnAllCorrectValues()
        {
            var provider = new Mock<MongoDefaultProvider>(ConnectionString).Object;

            const string expected = "collection";
            var actual = provider.Collection<Models.TestModel>("collection");

            Assert.Equal(expected, actual.CollectionNamespace.CollectionName);
            Assert.Equal("database.collection", actual.CollectionNamespace.FullName);
        }
    }
}