using Microsoft.Extensions.Configuration;
using PkkService.Queries.Application.PkkServices;
using PkkService.Queries.Infrastructure.PkkServices;
using PkkService.Queries.Infrastructure.PkkServices.Features;
using PkkService.Queries.Infrastructure.PkkServices.Typeahead;
using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;

namespace PkkService.Queries
{
    /// <summary>
    /// Класс, содержащий метод расширения, конфигурирующий DI контейнер для PkkService.Queries
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Добавить DI конфигурацию для PkkService.Queries
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddPkkServiceQueries(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var pkkServicesOptions = configuration.GetSection("PkkServicesOptions");
            services.Configure<PkkServicesOptions>(pkkServicesOptions);

            var retryPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .Or<TimeoutRejectedException>()
                .Or<TaskCanceledException>() 
                .WaitAndRetryAsync(new[]
                    {
                        TimeSpan.FromSeconds(1),
                        TimeSpan.FromSeconds(5),
                        TimeSpan.FromSeconds(10)
                    });

            var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(10);

            services.AddHttpClient("pkk", (provider, client) =>
            {
                var options = provider.GetService<IOptions<PkkServicesOptions>>()?.Value
                   ?? throw new ArgumentNullException(nameof(PkkServicesOptions));

                client.BaseAddress = new Uri(options.BaseUrl);
                client.Timeout = TimeSpan.FromSeconds(options.RequestTimeout ?? 30);
            })
            .AddPolicyHandler(retryPolicy)
            .AddPolicyHandler(timeoutPolicy);

            services.AddMediatR(Assembly.GetAssembly(typeof(Startup)));

            services.AddTransient<IPkkTypeaheadService, PkkTypeaheadService>();
            services.AddTransient<IPkkFeaturesService, PkkFeaturesService>();

            return services;
        }
    }
}
