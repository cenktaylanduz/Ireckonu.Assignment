using Ireckonu.Assignment.Data.Repositories;
using Ireckonu.Assignment.Data.Repositories.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Ireckonu.Assignment.Data
{
    public static class DataInjector
    {
        private static readonly bool _injected = false;

        public static void InjectRepositories(IServiceCollection services)
        {
            if (_injected)
                return;

            services.AddTransient<IDbDataRepository, DbDataRepository>();
            services.AddTransient<IJsonDataRepository, JsonDataRepository>();
            services.AddTransient<IFileQueueRepository, FileQueueRepository>();
        }
    }
}