using Microsoft.EntityFrameworkCore;
using ServiceManagerBackEnd.Models;

namespace ServiceManagerBackEnd.Repositories;

public class ServiceManagerContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public ServiceManagerContext(DbContextOptions<ServiceManagerContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasQueryFilter(p => !p.IsDeleted);
    }
}