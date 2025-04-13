using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Inventory.Service.Domain.AgregatesRoots;
namespace Inventory.Service.Infraestructure.Persistance.Context;

public class ApplicationDbContext : DbContext
{
    #region Sets

    public DbSet<Domain.AgregatesRoots.Inventory>  Inventories { get; set; }
    public DbSet<InventoryItem>  InventoriesItems { get; set; }

    #endregion

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}