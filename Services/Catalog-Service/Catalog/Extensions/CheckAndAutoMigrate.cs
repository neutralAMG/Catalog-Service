using Catalog.Service.Infraestructure.Persistance.Context.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;


namespace Catalog.Service.Api.Extensions;

public static class CheckAndAutoMigrate
{
    public static WebApplication AddCheckAndAutoMigrate(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        
        IServiceProvider provider = scope.ServiceProvider;
        ILogger<ApplicationContext> _logger = scope.ServiceProvider.GetRequiredService<ILogger<ApplicationContext>>();
        try
        {
            DatabaseFacade databaseFacade = provider.GetRequiredService<ApplicationContext>().Database;

            databaseFacade.EnsureCreated();

            if (databaseFacade.GetPendingMigrations().Any())
            {
                _logger.LogInformation("Migrating database");
                databaseFacade.Migrate();
                _logger.LogInformation("Migrated the database successfully ");
            }

        }
        catch (Exception e)
        {
            string message = e.ToString();
            _logger.LogError("Error while applying Migrations: {message}", message);
        }
        return  app;
    }
}