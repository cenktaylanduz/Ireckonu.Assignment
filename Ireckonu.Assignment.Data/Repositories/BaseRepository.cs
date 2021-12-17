using System.Collections.Generic;
using System.Threading.Tasks;
using Ireckonu.Assignment.Data.Entities;
using Ireckonu.Assignment.Data.Repositories.Abstractions;

namespace Ireckonu.Assignment.Data.Repositories
{
    public abstract class BaseRepository : IDataRepository
    {
        public abstract Task InsertMultipleRawDataAsync(List<RawData> entityList);
    }
}