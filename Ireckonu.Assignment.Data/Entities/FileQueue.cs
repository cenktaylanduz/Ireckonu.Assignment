using System;
using Ireckonu.Assignment.Data.Enums;

namespace Ireckonu.Assignment.Data.Entities
{
    public class FileQueue : BaseEntity
    {
        public string OriginalFilename { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public FileStatusType FileStatusType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
    }
}