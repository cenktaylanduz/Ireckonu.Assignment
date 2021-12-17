using Ireckonu.Assignment.Api.Config;
using Ireckonu.Assignment.Business;
using Ireckonu.Assignment.Business.Config;
using Ireckonu.Assignment.Data;
using Ireckonu.Assignment.Data.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Ireckonu.Assignment.Api
{
    /// <summary>
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        ///     This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            #region Configure Project Configs

            services.Configure<ApiConfig>(Configuration.GetSection("ApiConfig"));
            services.Configure<BusinessConfig>(Configuration.GetSection("BusinessConfig"));
            services.Configure<DataConfig>(Configuration.GetSection("DataConfig"));

            #endregion

            #region Configure Swagger

            var apiConfig = new ApiConfig();
            Configuration.Bind("ApiConfig", apiConfig);

            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = $"{apiConfig.Name} [{apiConfig.Environment}]",
                    Version = apiConfig.Version
                });
            });

            #endregion

            // Configures DI of repositories
            DataInjector.InjectRepositories(services);

            // Configures DI of services
            BusinessInjector.InjectServices(services);
        }

        /// <summary>
        ///     This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseStaticFiles();

            #region Swagger UI

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });

            // Enable middleware to serve swagger-ui (HTML, JS, TYS, etc.),
            // specifying the Swagger JSON endpoint.
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "StockMount Trendyol API");
            //    //c.RoutePrefix = string.Empty;
            //    //c.InjectStylesheet("/docs/bootstrap.min.css");
            //    //c.InjectStylesheet("/docs/sweetalert2.min.css");
            //    //c.InjectStylesheet("/docs/custom.css");
            //    //c.InjectJavascript("/docs/jquery.min.js");
            //    //c.InjectJavascript("/docs/bootstrap.min.js");
            //    //c.InjectJavascript("/docs/sweetalert2.all.min.js");
            //    //c.InjectJavascript("/docs/custom.js");
            //    c.DocumentTitle = env.ApplicationName;
            //});

            #endregion

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}