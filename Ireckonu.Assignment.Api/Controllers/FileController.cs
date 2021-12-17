using System.Collections.Generic;
using System.Threading.Tasks;
using Ireckonu.Assignment.Business.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Ireckonu.Assignment.Api.Controllers
{
    /// <summary>
    /// </summary>
    public class FileController : BaseController
    {
        private readonly IFileService _fileService;

        /// <summary>
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="fileService"></param>
        public FileController(
            ILogger<FileController> logger,
            IFileService fileService)
        {
            _logger = logger;
            _fileService = fileService;
        }

        /// <summary>
        ///     Upload File(s) and Enqueue To Process By Consumer
        /// </summary>
        /// <param name="formFileList"></param>
        /// <param name="enqueue"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Upload(List<IFormFile> formFileList, bool enqueue)
        {
            await _fileService.SaveFileAsync(formFileList, enqueue);

            return Ok("Your file will be processed a.s.a.p");
        }

        /// <summary>
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        [HttpGet]
        public string Test(string str)
        {
            return str.ToUpper();
        }
    }
}