using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using Newbody.Integration.Db.Mongo.Tests.Fixtures;
using Xunit;

namespace Newbody.Integration.Db.Mongo.Tests
{
    public class RepositoryTests
    {
        readonly Mock<ITestRepository<Models.TestModel>> _repository;

        public RepositoryTests()
        {
            _repository = new Mock<ITestRepository<Models.TestModel>>();
        }


        [Fact]
        public void ShouldInsertDocument()
        {
            var model = new Models.TestModel
            {
                Field1 = "Lorem ipsum",
                Field2 = true,
                Field3 = DateTime.Now
            };

            _repository.Setup(f => f.Insert(It.IsAny<Models.TestModel>()));

            _repository.Object.Insert(model);

            _repository.Verify(m => m.Insert(It.IsAny<Models.TestModel>()), Times.Once());

            Assert.Equal(model.Id, ObjectId.Parse("507f1f77bcf86cd799439011").ToString());
            Assert.Equal(model.CreatedAt.ToShortDateString(), DateTime.Now.AddMonths(-1).ToShortDateString());
            Assert.Equal(model.UpdatedAt.ToShortDateString(), DateTime.Now.ToShortDateString());
        }

        [Fact]
        public async Task ShouldInsertDocumentAsync()
        {
            var model = new Models.TestModel
            {
                Field1 = "Lorem ipsum",
                Field2 = true,
                Field3 = DateTime.Now
            };

            _repository.Setup(f => f.InsertAsync(It.IsAny<Models.TestModel>()));

            await _repository.Object.InsertAsync(model);

            _repository.Verify(m => m.InsertAsync(It.IsAny<Models.TestModel>()), Times.Once());
        }

        [Fact]
        public void ShouldUpdateDocument()
        {
            var model = new Models.TestModel
            {
                Id = ObjectId.GenerateNewId().ToString()
            };
            var filter = Builders<Models.TestModel>.Filter.Eq(x => x.Id, model.Id);
            var builder = Builders<Models.TestModel>.Update;
            var updates = new List<UpdateDefinition<Models.TestModel>>
            {
                builder.Set(x => x.Field1, "Nulla efficitur nisl sollicitudin")
            };

            _repository.Setup(f => f.Insert(It.IsAny<Models.TestModel>()));
            _repository.Object.Insert(model);
            _repository.Setup(f => f.Update(It.IsAny<FilterDefinition<Models.TestModel>>(), It.IsAny<UpdateDefinition<Models.TestModel>>(), It.IsAny<UpdateOptions>()));

            _repository.Object.Update(filter, builder.Combine(updates), new UpdateOptions());

            Assert.NotNull(_repository.Object.Find(filter));
        }

        [Fact]
        public async Task ShouldUpdateDocumentAsync()
        {
            var model = new Models.TestModel
            {
                Id = ObjectId.GenerateNewId().ToString()
            };
            var filter = Builders<Models.TestModel>.Filter.Eq(x => x.Id, model.Id);
            var builder = Builders<Models.TestModel>.Update;
            var updates = new List<UpdateDefinition<Models.TestModel>>
            {
                builder.Set(x => x.Field1, "Nulla efficitur nisl sollicitudin")
            };

            _repository.Setup(f => f.InsertAsync(It.IsAny<Models.TestModel>()));
            await _repository.Object.InsertAsync(model);
            _repository.Setup(f => f.UpdateAsync(It.IsAny<FilterDefinition<Models.TestModel>>(), It.IsAny<UpdateDefinition<Models.TestModel>>(), It.IsAny<UpdateOptions>()));

            await _repository.Object.UpdateAsync(filter, builder.Combine(updates), new UpdateOptions());

            Assert.NotNull(await _repository.Object.FindAsync(filter));
        }

        [Fact]
        public void ShouldDeleteDocument()
        {
            var model = new Models.TestModel();

            _repository.Setup(f => f.Delete(It.IsAny<Models.TestModel>()));

            _repository.Object.Delete(model);

            _repository.Verify(m => m.Delete(It.IsAny<Models.TestModel>()), Times.Once());
        }

        [Fact]
        public async Task ShouldDeleteDocumentAsync()
        {
            var model = new Models.TestModel();

            _repository.Setup(f => f.DeleteAsync(It.IsAny<Models.TestModel>()));

            await _repository.Object.DeleteAsync(model);

            _repository.Verify(m => m.DeleteAsync(It.IsAny<Models.TestModel>()), Times.Once());
        }
    }
}
