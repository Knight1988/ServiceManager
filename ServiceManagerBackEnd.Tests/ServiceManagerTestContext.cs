using Microsoft.EntityFrameworkCore;
using ServiceManagerBackEnd.Models;
using ServiceManagerBackEnd.Repositories;

namespace ServiceManagerBackEnd.Tests;

public class ServiceManagerTestContext : ServiceManagerContext
{
    public ServiceManagerTestContext(DbContextOptions<ServiceManagerContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().HasData(new User()
        {
            Id = 1,
            Name = "Demo",
            Username = "admin@demo.com",
            Password = Commons.Helper.EncryptPassword("demo@demo.com", "demo")
        });

        modelBuilder.Entity<Server>().HasData(new Server()
        {
            Id = 1,
            UserId = 1,
            Name = "Server 1",
            LastPing = DateTime.Now
        });

        modelBuilder.Entity<Server>().HasData(new Server()
        {
            Id = 2,
            UserId = 1,
            Name = "Server 1",
            LastPing = DateTime.Now,
            IsDeleted = true
        });
    }
}