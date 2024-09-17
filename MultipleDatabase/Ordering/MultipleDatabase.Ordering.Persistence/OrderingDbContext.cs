using Microsoft.EntityFrameworkCore;
using MultipleDatabase.Ordering.Domain.Orders;
using Shared;

namespace MultipleDatabase.Ordering.Persistence;

public class OrderingDbContext : DbContext
{
    private readonly Guid _tenantId;

    public OrderingDbContext(TenantProvider tenantProvider)
    {
        _tenantId = tenantProvider.GetTenantId();

        Database.EnsureCreated();
    }

    public DbSet<Order> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer($"Data Source=database;Initial Catalog=Ordering_{_tenantId.ToString().ToUpper()};User ID=sa;Password=P@sSw0rd!;TrustServerCertificate=true");

        base.OnConfiguring(optionsBuilder);
    }
}
