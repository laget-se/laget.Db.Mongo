using Moq;
using Xunit;

namespace Newbody.Integration.Db.Mongo.Tests
{
    public class ProviderTests
    {
        [Fact]
        public void ShouldReturnCorrectDatabaseName()
        {
            var provider = new Mock<MongoDefaultProvider>(new object[] { "mongodb://myDBReader:D1fficultP%40ssw0rd@mongodb0.example.com:27017/database?authSource=admin" }).Object;

            const string expected = "database";
            var actual = provider.GetDatabase();

            Assert.Equal(expected, actual.DatabaseNamespace.DatabaseName);
        }

        [Fact]
        public void ShouldReturnCorrectCollection()
        {
            var provider = new Mock<MongoDefaultProvider>(new object[] { "mongodb://myDBReader:D1fficultP%40ssw0rd@mongodb0.example.com:27017/database?authSource=admin" }).Object;

            const string expected = "collection";
            var actual = provider.GetCollection<Models.TestModel>("collection");

            Assert.Equal(expected, actual.CollectionNamespace.CollectionName);
            Assert.Equal("database.collection", actual.CollectionNamespace.FullName);
        }
    }
}
