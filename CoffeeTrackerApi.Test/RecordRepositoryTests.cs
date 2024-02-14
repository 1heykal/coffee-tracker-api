using CoffeeTrackerApi.Repositories;

namespace CoffeeTrackerApi.Test
{
    public class RecordRepositoryTests
    {
        [Fact]
        public void AddRecord_WhenRecordIdIsUnique_RecordShouldBeAdded()
        {
            // arrange
            var sut = new InMemoryDbContext();
            var repo = new RecordRepository(sut);

            // act
            var record = new Models.Record { Name = "HeyCool", Cost = 3000, ConsumingDate = DateTime.Now, Description = "Best chino i had, hot and sweet", Type = "Single" };

            repo.Add(record);

            // assert
            Assert.True(record.Id > 0);

        }
    }
}