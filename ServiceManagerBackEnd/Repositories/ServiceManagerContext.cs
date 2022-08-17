using Microsoft.EntityFrameworkCore;
using ServiceManagerBackEnd.Models;

namespace ServiceManagerBackEnd.Repositories;

public class ServiceManagerContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Server> Servers { get; set; }

    public ServiceManagerContext(DbContextOptions<ServiceManagerContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // define delete query filter
        modelBuilder.Entity<User>().HasQueryFilter(p => !p.IsDeleted);
        modelBuilder.Entity<Server>().HasQueryFilter(p => !p.IsDeleted);

        // define relationship
        modelBuilder.Entity<User>().HasMany(p => p.Servers)
            .WithOne(s => s.User)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}