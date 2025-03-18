using Catalog.Service.Domain.Repository;
using Catalog.Service.Infraestructure.Persistance;
using Catalog.Service.Infraestructure.Persistance.Context.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Catalog.Service.Infraestructure.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructurLayer(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ApplicationContext>(options =>
            {
      
                if(configuration.GetSection("isDev").Value == "true")
                {
                   options.UseInMemoryDatabase(configuration.GetConnectionString("inMemory"));
                }
                else
                {
                    options.UseSqlServer(configuration.GetConnectionString("Default"),
                        m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName) );
                }
               
            });
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

            return services;
        }


    }
}
