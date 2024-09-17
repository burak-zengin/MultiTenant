using Microsoft.EntityFrameworkCore;
using Shared;
using SingleDatabase.Catalog.Domain;
using SingleDatabase.Catalog.Domain.Products;

namespace SingleDatabase.Catalog.Persistence;

public class CatalogDbContext : DbContext
{
    private readonly Guid _tenantId;

    public CatalogDbContext(TenantProvider tenantProvider)
    {
        _tenantId = tenantProvider.GetTenantId();

        Database.EnsureCreated();
    }

    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=database;Initial Catalog=Catalog;User ID=sa;Password=P@sSw0rd!;TrustServerCertificate=true");

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasQueryFilter(p => p.TenantId == _tenantId);

        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges()
    {
        var entries = ChangeTracker.Entries<ModelBase>();
        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.TenantId = _tenantId;
            }
        }

        return base.SaveChanges();
    }
}
