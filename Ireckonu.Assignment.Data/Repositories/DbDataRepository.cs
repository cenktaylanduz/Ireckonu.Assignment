using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ireckonu.Assignment.Data.Config;
using Ireckonu.Assignment.Data.Entities;
using Ireckonu.Assignment.Data.Repositories.Abstractions;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Ireckonu.Assignment.Data.Repositories
{
    public class DbDataRepository : BaseRepository, IDbDataRepository
    {
        private const string RAW_DATA_COLLECTION_KEY = "raw_data";
        private readonly IMongoCollection<RawData> _collection;

        public DbDataRepository(IOptions<DataConfig> dataConfig)
        {
            var client = new MongoClient(dataConfig.Value.MongoConnectionString);
            var database = client.GetDatabase(dataConfig.Value.MongoDbName);

            _collection = database.GetCollection<RawData>(RAW_DATA_COLLECTION_KEY);
        }

        public override async Task InsertMultipleRawDataAsync(List<RawData> entityList)
        {
            //remove existing items by key
            var existingFilter = Builders<RawData>.Filter.In(x => x.Key, entityList.Select(x => x.Key));
            var existList = await _collection.Find(existingFilter).ToListAsync();

            var tmpEntityList = new List<RawData>();
            tmpEntityList.AddRange(entityList);
            tmpEntityList.RemoveAll(item => existList.Select(x => x.Key).Contains(item.Key));

            if (tmpEntityList.Count > 0)
                await _collection.InsertManyAsync(tmpEntityList);
        }
    }
}