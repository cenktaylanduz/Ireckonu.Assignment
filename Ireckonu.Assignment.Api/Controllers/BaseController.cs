using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Ireckonu.Assignment.Api.Controllers
{
    /// <summary>
    ///     Abstract Base Controller
    /// </summary>
    [ApiController]
    [Route("[controller]/[action]")]
    public abstract class BaseController : ControllerBase
    {
        /// <summary>
        /// </summary>
        protected ILogger<FileController> _logger;
    }
}