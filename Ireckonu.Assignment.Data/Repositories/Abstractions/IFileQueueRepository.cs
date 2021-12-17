using System.Threading.Tasks;
using Ireckonu.Assignment.Data.Entities;
using Ireckonu.Assignment.Data.Enums;

namespace Ireckonu.Assignment.Data.Repositories.Abstractions
{
    /// <summary>
    ///     Product database operations repo
    /// </summary>
    public interface IFileQueueRepository
    {
        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void AddToQueue(string id);

        /// <summary>
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task Create(FileQueue entity);

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fileStatusType"></param>
        /// <returns></returns>
        Task Update(string id, FileStatusType fileStatusType);
    }
}