namespace Ireckonu.Assignment.Business.Config
{
    /// <summary>
    /// </summary>
    public class BusinessConfig
    {
        /// <summary>
        ///     Destination Folder where the files will be uploaded
        /// </summary>
        public string FileUploadPath { get; set; }

        /// <summary>
        /// </summary>
        public int BatchSize { get; set; }
    }
}