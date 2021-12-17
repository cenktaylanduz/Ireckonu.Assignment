using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Ireckonu.Assignment.Data.Config;
using Ireckonu.Assignment.Data.Entities;
using Ireckonu.Assignment.Data.Repositories.Abstractions;
using Microsoft.Extensions.Options;

namespace Ireckonu.Assignment.Data.Repositories
{
    public class JsonDataRepository : BaseRepository, IJsonDataRepository
    {
        private readonly DataConfig _dataConfig;

        public JsonDataRepository(IOptions<DataConfig> dataConfig)
        {
            _dataConfig = dataConfig.Value;
        }

        public override async Task InsertMultipleRawDataAsync(List<RawData> entityList)
        {
            var jsonDataList = new List<RawData>();
            var path = Path.Combine(_dataConfig.JsonDataFilePath, _dataConfig.JsonDataFilename);

            var fileStr = string.Empty;

            if (!File.Exists(path))
            {
                var file = File.Create(path);
                file.Close();
            }
            else
            {
                fileStr = await File.ReadAllTextAsync(path);
            }

            if (!string.IsNullOrWhiteSpace(fileStr)) jsonDataList = JsonSerializer.Deserialize<List<RawData>>(fileStr);

            var tmpEntityList = new List<RawData>();
            tmpEntityList.AddRange(entityList);
            tmpEntityList.RemoveAll(item => jsonDataList.Select(x => x.Key).Contains(item.Key));

            if (tmpEntityList.Count > 0)
            {
                jsonDataList.AddRange(tmpEntityList);
                await File.WriteAllTextAsync(path, JsonSerializer.Serialize(jsonDataList));
            }
        }
    }
}