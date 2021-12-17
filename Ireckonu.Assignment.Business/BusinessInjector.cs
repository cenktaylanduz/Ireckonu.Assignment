using Ireckonu.Assignment.Business.Services;
using Ireckonu.Assignment.Business.Services.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Ireckonu.Assignment.Business
{
    public static class BusinessInjector
    {
        private static readonly bool _injected = false;

        public static void InjectServices(IServiceCollection services)
        {
            if (_injected)
                return;

            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IDataService, DataService>();
        }
    }
}