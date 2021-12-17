using System.Collections.Generic;
using System.Threading.Tasks;
using Ireckonu.Assignment.Data.Entities;

namespace Ireckonu.Assignment.Business.Services.Abstractions
{
    /// <summary>
    /// </summary>
    public interface IDataService
    {
        /// <summary>
        /// </summary>
        /// <param name="entityList"></param>
        /// <returns></returns>
        Task InsertMultipleRawDataAsync(List<RawData> entityList);
    }
}