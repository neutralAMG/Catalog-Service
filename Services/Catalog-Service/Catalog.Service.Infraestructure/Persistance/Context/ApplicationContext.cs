using Microsoft.EntityFrameworkCore;


namespace Catalog.Service.Infraestructure.Persistance.Context.Core
{
    public class ApplicationContext : DbContext
    {
        #region Sets

        #endregion

        public ApplicationContext()
        {
            
        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
        }
    }
}
