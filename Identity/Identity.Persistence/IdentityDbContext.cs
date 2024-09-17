using Microsoft.EntityFrameworkCore;
using Identity.Domain.Users;

namespace Identity.Persistence;

public class IdentityDbContext : DbContext
{
    public IdentityDbContext()
    {
        Database.EnsureCreated();
    }

    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=database;Initial Catalog=Identity;User ID=sa;Password=P@sSw0rd!;TrustServerCertificate=true");

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData([new User {
            Id = Guid.NewGuid(),
            Username = "User1",
            Password = "123456"
        }, new User {
            Id = Guid.NewGuid(),
            Username = "User2",
            Password = "123456"
        }, new User {
            Id = Guid.NewGuid(),
            Username = "User3",
            Password = "123456"
        }]);

        base.OnModelCreating(modelBuilder);
    }
}
