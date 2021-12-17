using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Ireckonu.Assignment.Business.Config;
using Ireckonu.Assignment.Business.Services;
using Ireckonu.Assignment.Business.Services.Abstractions;
using Ireckonu.Assignment.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Ireckonu.Assignment.Services.Tests
{
    public class FileServiceTest
    {
        public FileServiceTest()
        {
            LoggerMock = new Mock<ILogger<FileService>>();
            var businessOptions = new BusinessConfig
            {
                BatchSize = 10,
                FileUploadPath = @"c:\Ireckonu\File"
            };
            BusinessConfigMock = new Mock<IOptions<BusinessConfig>>();
            BusinessConfigMock.Setup(ap => ap.Value).Returns(businessOptions);
            FileQueueRepositoryMock = new Mock<IFileQueueRepository>();
            DataServiceMock = new Mock<IDataService>();
        }

        private Mock<ILogger<FileService>> LoggerMock { get; }
        private Mock<IOptions<BusinessConfig>> BusinessConfigMock { get; }
        private Mock<IFileQueueRepository> FileQueueRepositoryMock { get; }

        private Mock<IDataService> DataServiceMock { get; }

        //TODO we should not read file from the local IO subsystem, because of the time constrain
        // I implemented like below for now.
        private static List<IFormFile> SetupData()
        {
            var formFileList = new Mock<List<IFormFile>>();
            var fileMock = new Mock<IFormFile>();
            var content = File.ReadAllText("test.csv");
            var fileName = "test.csv";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);
            formFileList.Object.Add(fileMock.Object);
            return formFileList.Object;
        }

        [Fact]
        public async Task Given_ListOfFile_ShouldProcessFileWithOutEnque_And_ReturnsVoid()
        {
            // Arrange;
            var enqueue = false;
            var formFileList = SetupData();
            // Act
            var fileService = new FileService(LoggerMock.Object, BusinessConfigMock.Object,
                FileQueueRepositoryMock.Object, DataServiceMock.Object);
            await fileService.SaveFileAsync(formFileList, enqueue);

            // Assert
            Assert.Equal(Task.CompletedTask, Task.CompletedTask);
        }

        [Fact]
        public async Task Given_ListOfFile_ShouldProcessFileWithOutEnque_And_ThrowException()
        {
            // Arrange;
            var enqueue = false;
            List<IFormFile> formFileList = null;
            Exception e = null;
            // Act
            var fileService = new FileService(LoggerMock.Object, BusinessConfigMock.Object,
                FileQueueRepositoryMock.Object, DataServiceMock.Object);
            // Assert
            try
            {
                await fileService.SaveFileAsync(formFileList, enqueue);
            }
            catch (Exception ex)
            {
                e = ex;
            }

            Assert.True(e is not null);
        }
    }
}