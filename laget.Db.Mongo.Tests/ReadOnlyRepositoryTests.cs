using System.Collections.Generic;
using System.Threading.Tasks;
using laget.Db.Mongo.Exceptions;
using laget.Db.Mongo.Tests.Fixtures;
using laget.Db.Mongo.Tests.Models;
using MongoDB.Driver;
using Xunit;

namespace laget.Db.Mongo.Tests
{
    public class ReadOnlyRepositoryTests : IClassFixture<ReadOnlyRepositoryFixture<TestModel>>
    {
        private readonly ReadOnlyTestRepository<TestModel> _repository;

        public ReadOnlyRepositoryTests(ReadOnlyRepositoryFixture<TestModel> fixture)
        {
            _repository = fixture.Repository;
        }

        [Fact]
        public void ShouldThrowExceptionOnEmptyListInsert()
        {
            var exception = Assert.Throws<ReadOnlyException>(() => _repository.Insert(new List<TestModel>()));
            Assert.Equal("We're not allowing writes to a read-only repository", exception.Message);
        }

        [Fact]
        public async Task ShouldThrowExceptionOnEmptyListInsertAsync()
        {
            var exception = await Assert.ThrowsAsync<ReadOnlyException>(() => _repository.InsertAsync(new List<TestModel>()));
            Assert.Equal("We're not allowing writes to a read-only repository", exception.Message);
        }

        [Fact]
        public void ShouldThrowExceptionOnListInsert()
        {
            var exception = Assert.Throws<ReadOnlyException>(() => _repository.Insert(new List<TestModel> { new TestModel(), new TestModel() }));
            Assert.Equal("We're not allowing writes to a read-only repository", exception.Message);
        }

        [Fact]
        public async Task ShouldThrowExceptionOnListInsertAsync()
        {
            var exception = await Assert.ThrowsAsync<ReadOnlyException>(() => _repository.InsertAsync(new List<TestModel> { new TestModel(), new TestModel() }));
            Assert.Equal($"We're not allowing writes to a read-only repository", exception.Message);
        }

        [Fact]
        public void ShouldThrowExceptionOnInsert()
        {
            var exception = Assert.Throws<ReadOnlyException>(() => _repository.Insert(new TestModel()));
            Assert.Equal($"We're not allowing writes to a read-only repository: {nameof(TestModel)}", exception.Message);
        }

        [Fact]
        public async Task ShouldThrowExceptionOnInsertAsync()
        {
            var exception = await Assert.ThrowsAsync<ReadOnlyException>(() => _repository.InsertAsync(new TestModel()));
            Assert.Equal($"We're not allowing writes to a read-only repository: {nameof(TestModel)}", exception.Message);
        }

        [Fact]
        public void ShouldThrowExceptionOnUpdate()
        {
            var options = new UpdateOptions();
            var filter = Builders<TestModel>.Filter.Eq(x => x.Id, "test");
            var update = Builders<TestModel>.Update.Set(x => x.Field1, "124");

            var exception = Assert.Throws<ReadOnlyException>(() => _repository.Update(filter, update, options));
            Assert.Equal($"We're not allowing writes to a read-only repository", exception.Message);
        }

        [Fact]
        public async Task ShouldThrowExceptionOnUpdateAsync()
        {
            var options = new UpdateOptions();
            var filter = Builders<TestModel>.Filter.Eq(x => x.Id, "test");
            var update = Builders<TestModel>.Update.Set(x => x.Field1, "124");

            var exception = await Assert.ThrowsAsync<ReadOnlyException>(() => _repository.UpdateAsync(filter, update, options));
            Assert.Equal("We're not allowing writes to a read-only repository", exception.Message);
        }

        [Fact]
        public void ShouldThrowExceptionOnEmptyListUpsert()
        {
            var exception = Assert.Throws<ReadOnlyException>(() => _repository.Upsert(new List<TestModel>()));
            Assert.Equal("We're not allowing writes to a read-only repository", exception.Message);
        }

        [Fact]
        public async Task ShouldThrowExceptionOnEmptyListUpsertAsync()
        {
            var exception = await Assert.ThrowsAsync<ReadOnlyException>(() => _repository.UpsertAsync(new List<TestModel>()));
            Assert.Equal("We're not allowing writes to a read-only repository", exception.Message);
        }

        [Fact]
        public void ShouldThrowExceptionOnListUpsert()
        {
            var exception = Assert.Throws<ReadOnlyException>(() => _repository.Upsert(new List<TestModel> { new TestModel(), new TestModel() }));
            Assert.Equal("We're not allowing writes to a read-only repository", exception.Message);
        }

        [Fact]
        public async Task ShouldThrowExceptionOnListUpsertAsync()
        {
            var exception = await Assert.ThrowsAsync<ReadOnlyException>(() => _repository.UpsertAsync(new List<TestModel> { new TestModel(), new TestModel() }));
            Assert.Equal($"We're not allowing writes to a read-only repository", exception.Message);
        }

        [Fact]
        public void ShouldThrowExceptionOnUpsert()
        {
            var exception = Assert.Throws<ReadOnlyException>(() => _repository.Upsert(new TestModel()));
            Assert.Equal($"We're not allowing writes to a read-only repository: {nameof(TestModel)}", exception.Message);
        }

        [Fact]
        public async Task ShouldThrowExceptionOnUpsertAsync()
        {
            var exception = await Assert.ThrowsAsync<ReadOnlyException>(() => _repository.UpsertAsync(new TestModel()));
            Assert.Equal($"We're not allowing writes to a read-only repository: {nameof(TestModel)}", exception.Message);
        }

        [Fact]
        public void ShouldThrowExceptionOnFilteredUpsert()
        {
            var filter = Builders<TestModel>.Filter.Eq(x => x.Field1, "test");

            var exception = Assert.Throws<ReadOnlyException>(() => _repository.Upsert(filter, new TestModel()));
            Assert.Equal($"We're not allowing writes to a read-only repository: {nameof(TestModel)}", exception.Message);
        }

        [Fact]
        public async Task ShouldThrowExceptionOnFilteredUpsertAsync()
        {
            var filter = Builders<TestModel>.Filter.Eq(x => x.Field1, "test");

            var exception = await Assert.ThrowsAsync<ReadOnlyException>(() => _repository.UpsertAsync(filter, new TestModel()));
            Assert.Equal($"We're not allowing writes to a read-only repository: {nameof(TestModel)}", exception.Message);
        }

        [Fact]
        public void ShouldThrowExceptionOnEmptyListDelete()
        {
            var exception = Assert.Throws<ReadOnlyException>(() => _repository.Delete(new List<TestModel>()));
            Assert.Equal("We're not allowing writes to a read-only repository", exception.Message);
        }

        [Fact]
        public async Task ShouldThrowExceptionOnEmptyListDeleteAsync()
        {
            var exception = await Assert.ThrowsAsync<ReadOnlyException>(() => _repository.DeleteAsync(new List<TestModel>()));
            Assert.Equal("We're not allowing writes to a read-only repository", exception.Message);
        }

        [Fact]
        public void ShouldThrowExceptionOnListDelete()
        {
            var exception = Assert.Throws<ReadOnlyException>(() => _repository.Delete(new List<TestModel> { new TestModel(), new TestModel() }));
            Assert.Equal("We're not allowing writes to a read-only repository", exception.Message);
        }

        [Fact]
        public async Task ShouldThrowExceptionOnListDeleteAsync()
        {
            var exception = await Assert.ThrowsAsync<ReadOnlyException>(() => _repository.DeleteAsync(new List<TestModel> { new TestModel(), new TestModel() }));
            Assert.Equal($"We're not allowing writes to a read-only repository", exception.Message);
        }

        [Fact]
        public void ShouldThrowExceptionOnDelete()
        {
            var exception = Assert.Throws<ReadOnlyException>(() => _repository.Delete(new TestModel()));
            Assert.Equal($"We're not allowing writes to a read-only repository: {nameof(TestModel)}", exception.Message);
        }

        [Fact]
        public async Task ShouldThrowExceptionOnDeleteAsync()
        {
            var exception = await Assert.ThrowsAsync<ReadOnlyException>(() => _repository.DeleteAsync(new TestModel()));
            Assert.Equal($"We're not allowing writes to a read-only repository: {nameof(TestModel)}", exception.Message);
        }

        [Fact]
        public void ShouldThrowExceptionOnFilteredDelete()
        {
            var filter = Builders<TestModel>.Filter.Eq(x => x.Field1, "test");

            var exception = Assert.Throws<ReadOnlyException>(() => _repository.Delete(filter));
            Assert.Equal($"We're not allowing writes to a read-only repository", exception.Message);
        }

        [Fact]
        public async Task ShouldThrowExceptionOnFilteredDeleteAsync()
        {
            var filter = Builders<TestModel>.Filter.Eq(x => x.Field1, "test");

            var exception = await Assert.ThrowsAsync<ReadOnlyException>(() => _repository.DeleteAsync(filter));
            Assert.Equal($"We're not allowing writes to a read-only repository", exception.Message);
        }
    }
}
