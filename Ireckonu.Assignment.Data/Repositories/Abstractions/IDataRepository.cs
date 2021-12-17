using System.Collections.Generic;
using System.Threading.Tasks;
using Ireckonu.Assignment.Data.Entities;

namespace Ireckonu.Assignment.Data.Repositories.Abstractions
{
    /// <summary>
    ///     Raw Data Db Operations Repo
    /// </summary>
    public interface IDataRepository
    {
        /// <summary>
        ///     Insert Raw Data
        /// </summary>
        /// <param name="entityList"></param>
        /// <returns></returns>
        Task InsertMultipleRawDataAsync(List<RawData> entityList);
    }
}