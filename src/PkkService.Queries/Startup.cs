using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using PkkService.Queries.StartupConfigure;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PkkService.Queries
{

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                options.IncludeXmlComments(xmlPath);
            });

            services.AddPkkServiceQueries(Configuration);

            services.AddSwaggerGenNewtonsoftSupport();
            services.AddHttpClient();
            services.AddHttpContextAccessor();

            ConfigureControllersCore(services);

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.AddHealthChecks();

        }
        /// <summary>
        /// Перегружаемый метод для конфигурации приложения
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        protected virtual IMvcBuilder ConfigureControllersCore(IServiceCollection services)
        {

            // Конфигурируем стандартный REST API
            var builder = services.AddControllers(options =>
            {
                options.InputFormatters.RemoveType<Microsoft.AspNetCore.Mvc.Formatters.SystemTextJsonInputFormatter>();
                options.OutputFormatters.RemoveType<Microsoft.AspNetCore.Mvc.Formatters.SystemTextJsonOutputFormatter>();
            })
                .AddNewtonsoftJson(opts =>
                {
                    opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    opts.SerializerSettings.Converters.Add(new StringEnumConverter(new CamelCaseNamingStrategy()));
                    opts.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                });

            return builder;
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler(err => UseCustomExceptionHandling(err, env));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.RoutePrefix = "swagger";
                options.SwaggerEndpoint($"v1/swagger.json", "PkkService.Queries API");
            });

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseMiddleware(typeof(ErrorLoggingMiddleware));
            app.UseCors("CorsPolicy");
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();

                endpoints.MapHealthChecks("/health");
            });
        }

        /// <summary>
        /// Конфигурация обработчиков исключений
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        protected virtual void UseCustomExceptionHandling(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware(typeof(CustomErrorResponseMiddleware));
        }

    }
}
