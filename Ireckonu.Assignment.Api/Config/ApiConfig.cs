namespace Ireckonu.Assignment.Api.Config
{
    /// <summary>
    /// </summary>
    public class ApiConfig
    {
        /// <summary>
        ///     App Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     App Version
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        ///     Environment (dev,prod,staging)
        /// </summary>
        public string Environment { get; set; }
    }
}