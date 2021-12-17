using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ireckonu.Assignment.Business.Config;
using Ireckonu.Assignment.Business.Services.Abstractions;
using Ireckonu.Assignment.Data.Entities;
using Ireckonu.Assignment.Data.Enums;
using Ireckonu.Assignment.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Ireckonu.Assignment.Business.Services
{
    /// <inheritdoc cref="IFileService" />
    public class FileService : IFileService
    {
        private readonly BusinessConfig _businessConfig;
        private readonly IDataService _dataService;
        private readonly IFileQueueRepository _fileQueueRepository;
        private readonly ILogger<FileService> _logger;


        public FileService(
            ILogger<FileService> logger,
            IOptions<BusinessConfig> businessConfig,
            IFileQueueRepository fileQueueRepository,
            IDataService dataService)
        {
            _logger = logger;
            _businessConfig = businessConfig.Value;
            _fileQueueRepository = fileQueueRepository;
            _dataService = dataService;
        }

        public async Task SaveFileAsync(List<IFormFile> formFileList, bool enqueue)
        {
            var saveFileTaskList = formFileList.Select(file => SaveFileInternalAsync(file, enqueue)).ToList();
            foreach (var saveFileTask in saveFileTaskList) await saveFileTask;
        }

        private async Task SaveFileInternalAsync(IFormFile formFile, bool enqueue)
        {
            try
            {
                //enqueue file for import by consumer
                if (enqueue)
                {
                    await EnqueueFileAsync(formFile);
                    return;
                }

                await ProcessFileAsync(formFile);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        private async Task InsertFileQueueAsync(FileQueue entity)
        {
            await _fileQueueRepository.Create(entity);
        }

        private async Task EnqueueFileAsync(IFormFile formFile)
        {
            var extension = Path.GetExtension(formFile.FileName);

            //Create new filename with [DATE]_[UNIQUE IDENTIFIER].[EXTENSION]
            var newFilename = $"{DateTime.UtcNow:yyyy_mm_dd_HH_mm}_{Guid.NewGuid():N}{extension}";

            _logger.LogInformation($"{newFilename} - Saving file started");

            var fullPath = Path.Combine(_businessConfig.FileUploadPath, newFilename);

            await using var ms = File.Create(fullPath);
            await formFile.CopyToAsync(ms);

            _logger.LogInformation($"{newFilename} - Saving file end");

            var fileQueueEntity = new FileQueue
            {
                OriginalFilename = formFile.FileName,
                FileName = newFilename,
                FilePath = _businessConfig.FileUploadPath,
                FileStatusType = FileStatusType.New,
                CreatedAt = DateTime.UtcNow
            };

            await InsertFileQueueAsync(fileQueueEntity);
        }

        private async Task ProcessFileAsync(IFormFile formFile)
        {
            var ms = formFile.OpenReadStream();
            var tmpDataList = new List<RawData>();

            ms.Position = 0;
            using var reader = new StreamReader(ms, Encoding.UTF8);

            string row;
            var firstRow = true;
            while ((row = await reader.ReadLineAsync()) != null)
            {
                if (firstRow)
                {
                    firstRow = false;
                    continue;
                }

                var entity = new RawData();
                entity.FromDelimitedString(row, ',');
                tmpDataList.Add(entity);

                if (tmpDataList.Count == _businessConfig.BatchSize)
                {
                    await _dataService.InsertMultipleRawDataAsync(tmpDataList);
                    tmpDataList.Clear();
                }
            }

            //if rawData remains in tmpList write to db/json
            if (tmpDataList.Count > 0) await _dataService.InsertMultipleRawDataAsync(tmpDataList);
        }
    }
}