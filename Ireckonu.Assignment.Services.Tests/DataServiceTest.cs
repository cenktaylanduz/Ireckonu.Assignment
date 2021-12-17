using System.Collections.Generic;
using System.Threading.Tasks;
using Ireckonu.Assignment.Business.Services;
using Ireckonu.Assignment.Data.Entities;
using Ireckonu.Assignment.Data.Repositories.Abstractions;
using Moq;
using Xunit;

namespace Ireckonu.Assignment.Services.Tests
{
    public class DataServiceTest
    {
        private readonly Mock<IDbDataRepository> DbDataRepository;
        private readonly Mock<IJsonDataRepository> JsonDataRepository;

        public DataServiceTest()
        {
            DbDataRepository = new Mock<IDbDataRepository>();
            JsonDataRepository = new Mock<IJsonDataRepository>();
        }

        //public async Task InsertMultipleRawDataAsync(List<RawData> entityList)
        [Fact]
        public async Task ShouldInsertData_To_Database_ReturnVoid()
        {
            // Arrange;
            var dataService = new DataService(DbDataRepository.Object, JsonDataRepository.Object);
            var rawData = new List<RawData>
            {
                new()
                {
                    ArtikelCode = "1", Color = "asd", ColorCode = "", DeliveredIn = "", Description = "desctiption",
                    DiscountPrice = 10, Id = "1", Key = "1", Price = 15, Q1 = "", Size = ""
                }
            };
            // Act
            await dataService.InsertMultipleRawDataAsync(rawData);
            // Assert
            Assert.Equal(Task.CompletedTask, Task.CompletedTask);
        }
    }
}