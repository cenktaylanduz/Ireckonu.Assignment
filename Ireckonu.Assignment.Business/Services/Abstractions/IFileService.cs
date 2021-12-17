using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Ireckonu.Assignment.Business.Services.Abstractions
{
    /// <summary>
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        ///     Save File(s) and Enqueue Process
        /// </summary>
        /// <param name="formFileList"></param>
        /// <param name="enqueue"></param>
        /// <returns></returns>
        Task SaveFileAsync(List<IFormFile> formFileList, bool enqueue);
    }
}