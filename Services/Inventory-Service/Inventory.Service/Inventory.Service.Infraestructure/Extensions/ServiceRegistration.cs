using System.Reflection;
using Inventory.Service.Application.Core;
using Inventory.Service.Infraestructure.Persistance.Context;
using Inventory.Service.Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Service.Infraestructure.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfraestructureLayer(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            if (configuration.GetSection("IsDev").Value == "true")
            {
                options.UseInMemoryDatabase("InMemoryDatabase");
            }
            else
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), 
                    md => md.MigrationsAssembly(Assembly.GetExecutingAssembly()));
            }
            
        });
        
        services.AddScoped(typeof(IRepository<,>),  typeof(Repository<,>));
        

        return services;
    }
}