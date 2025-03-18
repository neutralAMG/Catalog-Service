
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Catalog.Service.CrossCuttingConcerns.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddCrossCutting(this IServiceCollection services, IHostBuilder host)
        {
            host.UseSerilog((context, loggerConfig) =>
            {
                loggerConfig.ReadFrom.Configuration(context.Configuration);
            });
            return services;
        }

    }
}
