using Microsoft.EntityFrameworkCore;
using Entities.Models;
using Repositories.Config;
using System.Reflection;

namespace Repositories
{
    public class RepositoryContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public RepositoryContext(DbContextOptions<RepositoryContext> options)
        : base(options) // DbContext e veriyoruz burada direk
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // modelBuilder.ApplyConfiguration(new ProductConfig()); // Bu şekilde tek tek config dosyalarını ekleyebiliriz.
            // modelBuilder.ApplyConfiguration(new CategoryConfig());
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // Bu şekilde tüm config dosyalarını otomatik olarak ekleyebiliriz.
        }
    }
}