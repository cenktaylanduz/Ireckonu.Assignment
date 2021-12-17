namespace Ireckonu.Assignment.Data.Config
{
    /// <summary>
    /// </summary>
    public class DataConfig
    {
        /// <summary>
        /// </summary>
        public string JsonDataFilePath { get; set; }

        /// <summary>
        /// </summary>
        public string JsonDataFilename { get; set; }

        /// <summary>
        /// </summary>
        public string MongoConnectionString { get; set; }

        /// <summary>
        /// </summary>
        public string MongoDbName { get; set; }

        public RabbitMQConfig FileQueueConfig { get; set; }
    }
}