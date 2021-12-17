using System.Text;
using System.Threading.Tasks;
using Ireckonu.Assignment.Data.Config;
using Ireckonu.Assignment.Data.Entities;
using Ireckonu.Assignment.Data.Enums;
using Ireckonu.Assignment.Data.Repositories.Abstractions;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RabbitMQ.Client;

namespace Ireckonu.Assignment.Data.Repositories
{
    /// <inheritdoc cref="IDataRepository" />
    public class FileQueueRepository : IFileQueueRepository
    {
        private const string FILE_QUEUE_COLLECTION_KEY = "file_queue";
        private readonly IMongoCollection<FileQueue> _collection;
        private readonly DataConfig _dataConfig;

        public FileQueueRepository(IOptions<DataConfig> dataConfig)
        {
            _dataConfig = dataConfig.Value;

            var client = new MongoClient(dataConfig.Value.MongoConnectionString);
            var database = client.GetDatabase(dataConfig.Value.MongoDbName);

            _collection = database.GetCollection<FileQueue>(FILE_QUEUE_COLLECTION_KEY);
        }

        public async Task Create(FileQueue entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task Update(string id, FileStatusType fileStatusType)
        {
            var filter = Builders<FileQueue>.Filter;
            var recFilter = filter.Eq(x => x.Id, id);
            var update = Builders<FileQueue>.Update;
            await _collection.UpdateOneAsync(recFilter,
                update.Push($"{nameof(FileQueue.FileStatusType)}", fileStatusType));
        }

        public void AddToQueue(string id)
        {
            var factory = new ConnectionFactory
            {
                HostName = _dataConfig.FileQueueConfig.Host,
                UserName = _dataConfig.FileQueueConfig.Username,
                Password = _dataConfig.FileQueueConfig.Password,
                VirtualHost = _dataConfig.FileQueueConfig.VirtualHost
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            var body = Encoding.UTF8.GetBytes(id);
            channel.BasicPublish(_dataConfig.FileQueueConfig.Exchange, _dataConfig.FileQueueConfig.RoutingKey, null,
                body);
        }
    }
}